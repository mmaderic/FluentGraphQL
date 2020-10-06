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
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentGraphQL.Builder.Extensions
{
    internal static class GraphQLStatementExtensions
    {
        private static readonly Dictionary<RuntimeTypeHandle, Func<IEnumerable<IGraphQLStatement>, string, object>> _finders;

        static GraphQLStatementExtensions()
        {
            var graphQLValueStatementFinder = new KeyValuePair<RuntimeTypeHandle, Func<IEnumerable<IGraphQLStatement>, string, object>>
                    (typeof(GraphQLValueStatement).TypeHandle, FindGraphQLValueStatement);

            _finders = new Dictionary<RuntimeTypeHandle, Func<IEnumerable<IGraphQLStatement>, string, object>>
            {
                { graphQLValueStatementFinder.Key, graphQLValueStatementFinder.Value }
            };
        }

        public static TStatement Find<TStatement>(this IEnumerable<IGraphQLStatement> statements, string key)
        {
            var finderKey = typeof(TStatement).TypeHandle;
            return (TStatement) _finders[finderKey].Invoke(statements, key);
        }

        public static void ApplySelectStatement(
            this IGraphQLValueStatement graphQLValueStatement, IGraphQLSelectNode graphQLSelectNode, IGraphQLSelectNodeFactory graphQLSelectNodeFactory)
        {
            graphQLSelectNode.Deactivate();
            graphQLSelectNode.IsActive = true;

            ApplySelectStatementRecursive(graphQLValueStatement, graphQLSelectNode, graphQLSelectNodeFactory);
        }

        public static void ApplyIncludeStatement(
            this IGraphQLValueStatement graphQLValueStatement, IGraphQLSelectNode graphQLSelectNode, IGraphQLSelectNodeFactory graphQLSelectNodeFactory)
        {
            graphQLSelectNode.IsActive = true;

            ApplySelectStatementRecursive(graphQLValueStatement, graphQLSelectNode, graphQLSelectNodeFactory, activateProperties: true);
        }

        public static IEnumerable<IGraphQLValueStatement> ReplaceAt(this IEnumerable<IGraphQLValueStatement> collection, int index, IGraphQLValueStatement newItem)
        {
            var currentIndex = 0;
            foreach (var item in collection)
            {
                if (currentIndex != index)                
                    yield return item;                
                else                
                    yield return newItem;     
                
                currentIndex++;
            }
        }

        private static void ApplySelectStatementRecursive(
            IGraphQLValueStatement graphQLValueStatement, IGraphQLSelectNode graphQLSelectNode, IGraphQLSelectNodeFactory graphQLSelectNodeFactory, bool activateProperties = false)
        {
            if (graphQLValueStatement is null)
                return;

            if (graphQLValueStatement.Value is IGraphQLCollectionValue collectionValue)
            {
                if (!(graphQLValueStatement.PropertyName is null))
                {
                    var propertyName = graphQLValueStatement.PropertyName;
                    var includeStatement = propertyName.Contains("<graphql-include>");
                    if (includeStatement)
                        propertyName = propertyName.Replace("<graphql-include>", "");

                    var childNode = (IGraphQLSelectNode)graphQLSelectNode.Get(propertyName);
                    if (childNode is null)
                    {
                        childNode = GetCircuitedNode(graphQLSelectNode, propertyName, graphQLSelectNodeFactory);
                        graphQLSelectNode.ChildSelectNodes = graphQLSelectNode.ChildSelectNodes.Append(childNode);
                    }

                    graphQLSelectNode = childNode;
                    graphQLSelectNode.IsActive = true;
                    if (activateProperties || includeStatement)
                        graphQLSelectNode.Activate(recursive: false);
                }

                foreach (var item in collectionValue.CollectionItems)
                {
                    var objectValue = (IGraphQLObjectValue)item;
                    ApplySelectStatementRecursive(objectValue.PropertyValues.First(), graphQLSelectNode, graphQLSelectNodeFactory, activateProperties);
                }
            }
            else if (graphQLValueStatement.Value is IGraphQLObjectValue objectValue)
            {
                if (graphQLValueStatement.PropertyName is null)
                {
                    foreach (var item in objectValue.PropertyValues)
                        ApplySelectStatementRecursive(item, graphQLSelectNode, graphQLSelectNodeFactory, activateProperties);
                }
                else
                {
                    var propertyName = graphQLValueStatement.PropertyName;
                    var includeStatement = propertyName.Contains("<graphql-include>");
                    if (includeStatement)
                        propertyName = propertyName.Replace("<graphql-include>", "");

                    var childNode = (IGraphQLSelectNode)graphQLSelectNode.Get(propertyName);
                    if (childNode is null)
                    {
                        childNode = GetCircuitedNode(graphQLSelectNode, propertyName, graphQLSelectNodeFactory);
                        graphQLSelectNode.ChildSelectNodes = graphQLSelectNode.ChildSelectNodes.Append(childNode);
                    }

                    graphQLSelectNode = childNode;
                    graphQLSelectNode.IsActive = true;
                    if (activateProperties || includeStatement)
                        graphQLSelectNode.Activate(recursive: false);

                    ApplySelectStatementRecursive(objectValue.PropertyValues.First(), graphQLSelectNode, graphQLSelectNodeFactory, activateProperties);
                }
            }
            else
            {
                var selectStatement = graphQLSelectNode.Get(graphQLValueStatement.PropertyName);
                if (!(selectStatement is null))
                    selectStatement.Activate(recursive: false);
                else
                {
                    var circuitedNode = GetCircuitedNode(graphQLSelectNode, graphQLValueStatement.PropertyName, graphQLSelectNodeFactory);
                    if (!(circuitedNode is null))
                    {
                        graphQLSelectNode.ChildSelectNodes = graphQLSelectNode.ChildSelectNodes.Append(circuitedNode);
                        circuitedNode.Activate(recursive: false);
                    }
                }
            }                
        }

        private static IGraphQLSelectNode GetCircuitedNode(IGraphQLSelectNode graphQLSelectNode, string propertyName, IGraphQLSelectNodeFactory graphQLSelectNodeFactory)
        {
            var fullSelectNode = graphQLSelectNodeFactory.Get(graphQLSelectNode.EntityType);
            var requestedNode = fullSelectNode?.GetChildNode(propertyName);
            if (!(requestedNode is null))
            {
                fullSelectNode.Deactivate();
                requestedNode.SetHierarchyLevel(graphQLSelectNode.HeaderNode.HierarchyLevel);
                foreach (var node in requestedNode.ChildSelectNodes)
                    node.SetHierarchyLevel(requestedNode.HeaderNode.HierarchyLevel);

                return requestedNode;
            }

            return null;
        }

        private static object FindGraphQLValueStatement(IEnumerable<IGraphQLStatement> statements, string key)
        {
            return statements.FirstOrDefault(x => x is GraphQLValueStatement statement && statement.PropertyName.Equals(key));
        }
    }
}
