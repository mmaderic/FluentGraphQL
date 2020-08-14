/*
    MIT License

    Copyright (c) 2020 Mateo Mađerić

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
*/

using FluentGraphQL.Builder.Abstractions;
using FluentGraphQL.Builder.Atoms;
using FluentGraphQL.Builder.Extensions;
using FluentGraphQL.Builder.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FluentGraphQL.Builder.Factories
{
    public class GraphQLSelectNodeFactory : IGraphQLSelectNodeFactory
    {
        private class StatementContainer
        {
            public IEnumerable<IGraphQLPropertyStatement> PropertyStatements { get; set; }
            public IEnumerable<IGraphQLSelectNode> NestedObjectStatements { get; set; }
            public IEnumerable<IGraphQLSelectNode> NestedCollectionStatements { get; set; }
            public IEnumerable<IGraphQLSelectNode> NestedAggregateContainers { get; set; }           
        }

        public virtual IGraphQLSelectNode Construct(Type type, int hierarchyLevel, Type parentType = null, bool isCollectionNode = false, string explicitNodeName = null)
        {
            var nodeName = explicitNodeName is null
                ? type.Root().Name
                : explicitNodeName;

            var properties = type.IsInterface 
                ? type.GetInterfaces().SelectMany(x => x.GetProperties()).Concat(type.GetProperties())
                : type.GetProperties();

            var isAggregateContainer = typeof(IGraphQLAggregateContainer).IsAssignableFrom(type);
            var entityType = isAggregateContainer ? type.GetGenericArguments().First() : type;
            var headerNode = new GraphQLHeaderNode(nodeName, hierarchyLevel);

            var container = !type.Equals(typeof(IGraphQLAggregateClause))
                ? BuildNestedStatementsContainer(properties, type, parentType, hierarchyLevel, isAggregateContainer)
                : null;

            return BuildSelectNode(headerNode, container, entityType, isCollectionNode, !isAggregateContainer);
        }

        private StatementContainer BuildNestedStatementsContainer(IEnumerable<PropertyInfo> properties, Type type, Type parentType, int hierarchyLevel, bool isAggregateContainer)
        {
            var simpleProperties = properties.Where(x => IsSimpleProperty(x)).ToArray();
            var complexProperties = properties.Except(simpleProperties).ToArray();
            var CollectionProperties = complexProperties.Where(x => IsCollectionProperty(x)).ToArray();
            var AggregateContainerProperties = complexProperties.Except(CollectionProperties).Where(x => IsAggregateContainerProperty(x)).ToArray();
            var ObjectProperties = complexProperties.Except(CollectionProperties.Union(AggregateContainerProperties)).ToArray();

            var parentTypeArgument = isAggregateContainer ? parentType : type;

            var container = new StatementContainer();
            Parallel.Invoke(
                () => container.PropertyStatements = ConstructPropertyStatements(simpleProperties),
                () => container.NestedObjectStatements = ConstructNestedObjectSelectNodes(ObjectProperties, hierarchyLevel, parentTypeArgument, parentType),
                () => container.NestedCollectionStatements = ConstructNestedCollectionSelectNodes(CollectionProperties, hierarchyLevel, parentTypeArgument, parentType),
                () => container.NestedAggregateContainers = ConstructAggregateContainerSelectNodes(AggregateContainerProperties, hierarchyLevel, parentTypeArgument, parentType)
            );

            return container;
        }

        private IEnumerable<IGraphQLSelectNode> ConstructNestedObjectSelectNodes(
            IEnumerable<PropertyInfo> propertyInfos, int hierarchyLevel, Type type, Type parentType)
        {
            return propertyInfos.Where(x => !x.PropertyType.Equals(parentType))
                .Select(x => 
                {
                    var useExplicitName = 
                        x.PropertyType.Equals(typeof(IGraphQLAggregate)) ||
                        x.PropertyType.Equals(typeof(IGraphQLAggregateClause));

                    var explicitName = useExplicitName ? x.Name : null;
                    return Construct(x.PropertyType, hierarchyLevel + 1, type, false, explicitName);
                }).ToArray();
        }

        private IEnumerable<IGraphQLSelectNode> ConstructNestedCollectionSelectNodes(
            IEnumerable<PropertyInfo> propertyInfos, int hierarchyLevel, Type type, Type parentType)
        {
            return propertyInfos.Where(x => !x.PropertyType.GetGenericArguments().First().Equals(parentType))
                .Select(x => Construct(x.PropertyType.GetGenericArguments().First(), hierarchyLevel + 1, type, true, x.Name)).ToArray();
        }

        private IEnumerable<IGraphQLPropertyStatement> ConstructPropertyStatements(IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Select(x => new GraphQLPropertyStatement(x.Name)).ToArray();
        }

        private IEnumerable<IGraphQLSelectNode> ConstructAggregateContainerSelectNodes(
            IEnumerable<PropertyInfo> propertyInfos, int hierarchyLevel, Type type, Type parentType)
        {
            return propertyInfos.Where(x => !x.PropertyType.Equals(parentType))
                .Select(x => Construct(x.PropertyType, hierarchyLevel + 1, type, false, x.Name)).ToArray();
        }

        private bool IsSimpleProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsGenericType)
                return propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));

            var type = propertyInfo.PropertyType;
            return
                !typeof(IGraphQLEntity).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateContainer).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregate).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateClause).IsAssignableFrom(type);
        }

        private bool IsCollectionProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(ICollection<>));
        }

        private bool IsAggregateContainerProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(IGraphQLAggregateContainer<>));
        }

        private IGraphQLSelectNode BuildSelectNode(IGraphQLHeaderNode headerNode, StatementContainer container, Type entityType, bool isCollectionNode, bool isActive)
        {
            var childSelectNodes = !(container is null)
                ? container.NestedObjectStatements.Union(container.NestedCollectionStatements)
                : new List<IGraphQLSelectNode>();

            var propertyStatements = container?.PropertyStatements is null
                ? new List<IGraphQLPropertyStatement>()
                : container?.PropertyStatements;

            var nestedAggregateContainers = container?.NestedAggregateContainers is null
                ? new List<IGraphQLSelectNode>()
                : container?.NestedAggregateContainers;

            return new GraphQLSelectNode(
                headerNode,
                propertyStatements,
                childSelectNodes,
                nestedAggregateContainers,
                entityType,
                isCollectionNode,
                isActive);
        }
    }
}
