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
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Extensions;
using FluentGraphQL.Builder.Nodes;
using System;
using System.Collections;
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

        public virtual IGraphQLSelectNode Construct(Type type)
        {
            var selectNode = ConstructRecursive(type);
            var rootAggregateContainer = typeof(IGraphQLAggregateContainerNode<>).MakeGenericType(type);
            var rootAggregateNode = ConstructRecursive(rootAggregateContainer);

            rootAggregateNode.HeaderNode.Title = type.Name;
            rootAggregateNode.HeaderNode.Suffix = Constant.GraphQLKeyords.Aggregate;

            ((List<IGraphQLSelectNode>)selectNode.AggregateContainerNodes).Add(rootAggregateNode);

            return selectNode;
        }

        private IGraphQLSelectNode ConstructRecursive(Type type, int hierarchyLevel = 1, List<Type> parentTypes = null, bool isCollectionNode = false, string explicitNodeName = null)
        {
            if (parentTypes is null)
                parentTypes = new List<Type>();

            var nodeName = explicitNodeName is null
                ? type.Root().Name
                : explicitNodeName;

            var properties = type.IsInterface
                ? type.GetInterfaces().SelectMany(x => x.GetProperties()).Concat(type.GetProperties())
                : type.GetProperties();

            var isAggregateContainer = typeof(IGraphQLAggregateContainerNode).IsAssignableFrom(type);
            var entityType = isAggregateContainer ? type.GetGenericArguments().First() : type;
            var headerNode = new GraphQLHeaderNode(nodeName, hierarchyLevel);

            var container = !type.Equals(typeof(IGraphQLAggregateClauseNode))
                ? BuildNestedStatementsContainer(properties, type, parentTypes, hierarchyLevel, isAggregateContainer)
                : null;

            return BuildSelectNode(headerNode, container, entityType, isCollectionNode, !isAggregateContainer);
        }

        private StatementContainer BuildNestedStatementsContainer(IEnumerable<PropertyInfo> properties, Type type, List<Type> parentTypes, int hierarchyLevel, bool isAggregateContainer)
        {
            var simpleProperties = properties.Where(x => IsSimpleProperty(x)).ToArray();
            var complexProperties = properties.Except(simpleProperties).ToArray();
            var CollectionProperties = complexProperties.Where(x => IsCollectionProperty(x)).ToArray();
            var AggregateContainerProperties = complexProperties.Except(CollectionProperties).Where(x => IsAggregateContainerProperty(x)).ToArray();
            var ObjectProperties = complexProperties.Except(CollectionProperties.Union(AggregateContainerProperties)).ToArray();

            var parentTypeArgument = isAggregateContainer && parentTypes.Any() ? parentTypes.Last() : type;
            parentTypes.Add(parentTypeArgument);

            var container = new StatementContainer();
            Parallel.Invoke(
                () => container.PropertyStatements = ConstructPropertyStatements(simpleProperties),
                () => container.NestedObjectStatements = ConstructNestedObjectSelectNodes(ObjectProperties, hierarchyLevel, parentTypes),
                () => container.NestedCollectionStatements = ConstructNestedCollectionSelectNodes(CollectionProperties, hierarchyLevel, parentTypes),
                () => container.NestedAggregateContainers = ConstructAggregateContainerSelectNodes(AggregateContainerProperties, hierarchyLevel, parentTypes)
            );

            return container;
        }

        private IEnumerable<IGraphQLSelectNode> ConstructNestedObjectSelectNodes(
            IEnumerable<PropertyInfo> propertyInfos, int hierarchyLevel, List<Type> parentTypes)
        {
            return propertyInfos.Where(x => !parentTypes.Contains(x.PropertyType))
                .Select(x => 
                {
                    var useExplicitName = 
                        x.PropertyType.Equals(typeof(IGraphQLAggregateNode)) ||
                        x.PropertyType.Equals(typeof(IGraphQLAggregateClauseNode));

                    var explicitName = useExplicitName ? x.Name : null;
                    return ConstructRecursive(x.PropertyType, hierarchyLevel + 1, new List<Type>(parentTypes), false, explicitName);
                }).ToArray();
        }

        private IEnumerable<IGraphQLSelectNode> ConstructNestedCollectionSelectNodes(
            IEnumerable<PropertyInfo> propertyInfos, int hierarchyLevel, List<Type> parentTypes)
        {
            return propertyInfos.Where(x => !parentTypes.Contains(x.PropertyType.GetGenericArguments().First()))
                .Select(x => ConstructRecursive(x.PropertyType.GetGenericArguments().First(), hierarchyLevel + 1, new List<Type>(parentTypes), true, x.Name)).ToArray();
        }

        private IEnumerable<IGraphQLPropertyStatement> ConstructPropertyStatements(IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Select(x => new GraphQLPropertyStatement(x.Name)).ToArray();
        }

        private IEnumerable<IGraphQLSelectNode> ConstructAggregateContainerSelectNodes(
            IEnumerable<PropertyInfo> propertyInfos, int hierarchyLevel, List<Type> parentTypes)
        {
            return propertyInfos.Where(x => !parentTypes.Contains(x.PropertyType))
                .Select(x => ConstructRecursive(x.PropertyType, hierarchyLevel + 1, new List<Type>(parentTypes), false, x.Name)).ToList();
        }

        private bool IsSimpleProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsGenericType)
                return propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));

            var type = propertyInfo.PropertyType;
            return
                !typeof(IGraphQLEntity).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateContainerNode).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateNode).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateClauseNode).IsAssignableFrom(type);
        }

        private bool IsCollectionProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType &&
                typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
        }

        private bool IsAggregateContainerProperty(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType &&
                propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(IGraphQLAggregateContainerNode<>));
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
