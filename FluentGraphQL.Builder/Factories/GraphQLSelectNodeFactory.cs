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
using System.Collections.Concurrent;
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
            public IEnumerable<GraphQLPropertyStatement> PropertyStatements { get; set; }
            public IEnumerable<SelectNodeMetadata> NestedObjectStatements { get; set; }
            public IEnumerable<SelectNodeMetadata> NestedCollectionStatements { get; set; }
            public IEnumerable<SelectNodeMetadata> NestedAggregateContainers { get; set; }     
            
            public StatementContainer()
            {
                PropertyStatements = Enumerable.Empty<GraphQLPropertyStatement>();
                NestedObjectStatements = Enumerable.Empty<SelectNodeMetadata>();
                NestedCollectionStatements = Enumerable.Empty<SelectNodeMetadata>();
                NestedAggregateContainers = Enumerable.Empty<SelectNodeMetadata>();
            }           
        }  
        
        private class SelectNodeMetadata
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Type Type { get; set; }
            public int Level { get; set; }
            public string Title { get; set; }
            public string Suffix { get; set; }
            public bool IsCollection{ get; set; }
            public bool IsActive { get; set; }
            public StatementContainer Container { get; set; }   

            public SelectNodeMetadata(
                string name, Type type, int level, string title, bool isCollection, bool isActive, StatementContainer container)
            {
                Id = Guid.NewGuid();
                Name = name;
                Type = type;
                Level = level;
                Title = title;
                IsCollection = isCollection;
                IsActive = isActive;
                Container = container;
            }            

            public override string ToString()
            {
                return Name;
            }
        }

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IGraphQLSelectNode> _masterSelectNodeCache;

        static GraphQLSelectNodeFactory()
        {
            _masterSelectNodeCache = new ConcurrentDictionary<RuntimeTypeHandle, IGraphQLSelectNode>();
        }

        public virtual IGraphQLSelectNode Construct(Type type)
        {
            var key = type.TypeHandle;
            var exists = _masterSelectNodeCache.TryGetValue(key, out IGraphQLSelectNode selectNode);
            if (!exists)
            {
                var selectNodePath = new List<SelectNodeMetadata>();
                var rootAggregatePath = new List<SelectNodeMetadata>();
                var rootAggregateContainer = typeof(IGraphQLAggregateContainerNode<>).MakeGenericType(type);

                var selectNodeMetadata = ConstructMetadata(type, selectNodePath);
                var rootAggregateNodeMetadata = ConstructMetadata(rootAggregateContainer, rootAggregatePath);

                rootAggregateNodeMetadata.Title = type.Name;
                rootAggregateNodeMetadata.Suffix = Constant.GraphQLKeyords.Aggregate;

                selectNode = BuildSelectNode(selectNodeMetadata);
                var rootAggregateNode = BuildSelectNode(rootAggregateNodeMetadata);

                ((List<IGraphQLSelectNode>)selectNode.AggregateContainerNodes).Add(rootAggregateNode);

                _masterSelectNodeCache.TryAdd(key, selectNode);
            }

            return (IGraphQLSelectNode) selectNode.DeepCopy();
        }

        private SelectNodeMetadata ConstructMetadata(Type type, List<SelectNodeMetadata> path, int level = 1, bool isCollection = false, string propertyName = null)
        {
            var isAggregateContainer = typeof(IGraphQLAggregateContainerNode).IsAssignableFrom(type);
            var entityType = isAggregateContainer ? type.GetGenericArguments().First() : type;
            var title = propertyName is null ? type.Root().Name : propertyName;
            var name = $"{entityType.Name}.{title}";
           
            var circuit = path.FirstOrDefault(x => x.Name == name);
            if (!(circuit is null))
                return null;

            var active = IsInitiallyActiveNode(type, path);
            var container = new StatementContainer();
            var metadata = new SelectNodeMetadata(name, entityType, level, title, isCollection, active, container);
            path.Add(metadata);

            if (!type.Equals(typeof(IGraphQLAggregateClauseNode)))
                BuildStatementsContainer(container, type, level, path);

            return metadata;
        }

        private void BuildStatementsContainer(StatementContainer container, Type type, int level, List<SelectNodeMetadata> path)
        {
            var properties = type.IsInterface
                ? type.GetInterfaces().SelectMany(x => x.GetProperties()).Concat(type.GetProperties())
                : type.GetProperties();

            var simpleProperties = properties.Where(x => IsSimpleProperty(x)).ToArray();
            var complexProperties = properties.Except(simpleProperties).ToArray();
            var CollectionProperties = complexProperties.Where(x => IsCollectionProperty(x)).ToArray();
            var AggregateContainerProperties = complexProperties.Except(CollectionProperties).Where(x => IsAggregateContainerProperty(x)).ToArray();
            var ObjectProperties = complexProperties.Except(CollectionProperties.Union(AggregateContainerProperties)).ToArray();

            Parallel.Invoke(
                () => container.PropertyStatements = ConstructPropertyStatements(simpleProperties),
                () => container.NestedObjectStatements = ConstructNestedObjectSelectNodes(ObjectProperties, level, path),
                () => container.NestedCollectionStatements = ConstructNestedCollectionSelectNodes(CollectionProperties, level, path),
                () => container.NestedAggregateContainers = ConstructAggregateContainerSelectNodes(AggregateContainerProperties, level, path)
            );
        }

        private IEnumerable<GraphQLPropertyStatement> ConstructPropertyStatements(IEnumerable<PropertyInfo> propertyInfos)
        {
            return propertyInfos.Select(x => new GraphQLPropertyStatement(x.Name)).ToArray();
        }       

        private IEnumerable<SelectNodeMetadata> ConstructNestedObjectSelectNodes(IEnumerable<PropertyInfo> propertyInfos, int level, List<SelectNodeMetadata> path)
        {
            return propertyInfos.Select(x => ConstructMetadata(x.PropertyType, new List<SelectNodeMetadata>(path), level + 1, false, x.Name)).ToArray();
        }

        private IEnumerable<SelectNodeMetadata> ConstructNestedCollectionSelectNodes(IEnumerable<PropertyInfo> propertyInfos, int level, List<SelectNodeMetadata> path)
        {
            return propertyInfos.Select(x => ConstructMetadata(x.PropertyType.GetGenericArguments().First(), new List<SelectNodeMetadata>(path), level + 1, true, x.Name)).ToArray();
        }

        private IEnumerable<SelectNodeMetadata> ConstructAggregateContainerSelectNodes(IEnumerable<PropertyInfo> propertyInfos, int level, List<SelectNodeMetadata> path)
        {
            return propertyInfos.Select(x => ConstructMetadata(x.PropertyType, new List<SelectNodeMetadata>(path), level + 1, false, x.Name)).ToArray();
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

        private bool IsInitiallyActiveNode(Type type, List<SelectNodeMetadata> path)
        {
            return
                !typeof(IGraphQLAggregateContainerNode).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateNode).IsAssignableFrom(type) &&
                !typeof(IGraphQLAggregateClauseNode).IsAssignableFrom(type) &&
                !(typeof(IGraphQLEntity).IsAssignableFrom(type) && path.Count > 0)
;
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

        private IGraphQLSelectNode BuildSelectNode(SelectNodeMetadata metadata)
        {
            if (metadata is null)
                return null;

            var headerNode = new GraphQLHeaderNode(metadata.Title, metadata.Suffix, metadata.Level);
            var node = new GraphQLSelectNode(headerNode, metadata.Container.PropertyStatements, metadata.Type, metadata.IsCollection, metadata.IsActive);

            var nestedAggregateContainers = metadata.Container.NestedAggregateContainers.Select(x => BuildSelectNode(x)).Where(x => !(x is null)).ToList();
            var childSelectNodes = metadata.Container.NestedObjectStatements.Select(x => BuildSelectNode(x))
                    .Union(metadata.Container.NestedCollectionStatements.Select(x => BuildSelectNode(x))).Where(x => !(x is null)).ToArray();

            node.ChildSelectNodes = childSelectNodes;
            node.AggregateContainerNodes = nestedAggregateContainers;

            if (!metadata.IsActive)
                node.Deactivate();

            if (metadata.Type.Equals(typeof(IGraphQLAggregateClauseNode)))
                node.PropertyStatements = new List<IGraphQLPropertyStatement>();

            return node;
        }  
    }
}
