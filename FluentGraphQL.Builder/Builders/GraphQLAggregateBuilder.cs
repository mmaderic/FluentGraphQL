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

using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Builder.Abstractions;
using FluentGraphQL.Builder.Atoms;
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Constructs;
using FluentGraphQL.Builder.Extensions;
using FluentGraphQL.Builder.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Builders
{
    public class GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> : GraphQLQueryBuilder<TRoot, TEntity>,
        IGraphQLRootAggregateBuilder<TRoot>, IGraphQLRootAggregateNodesBuilder<TRoot>,
        IGraphQLSingleAggregateBuilder<TRoot, TAggregate>, IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>,
        IGraphQLStandardAggregateBuilder<TRoot, TAggregate>, IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
        where TAggregate : IGraphQLEntity
    {
        protected IGraphQLSelectNode _graphQLAggregateNodes;
        private IGraphQLQueryBuilder _parentBuilder;

        private Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder> _childAggregateBuilders;

        public GraphQLAggregateBuilder(
            IGraphQLQuery graphQLQuery, IGraphQLSelectNode graphQLSelectNode, 
            IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory) 
            : base(graphQLQuery, graphQLSelectNode, graphQLExpressionConverter, graphQLValuefactory)
        {
        }

        protected GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> Count()
        {
            _graphQLSelectNode.IsActive = true;

            var aggregateNode = _graphQLSelectNode.GetChildNode<IGraphQLAggregateNode>();
            aggregateNode.IsActive = true;
            aggregateNode.ActivateProperty(Constant.AggregateMethodCalls.Count);

            return this;
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Count()
        {
            return Count();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Count()
        {
            return Count();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Count()
        {
            return Count();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Count()
        {
            return Count();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Count()
        {
            return Count();
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Avg<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Avg<TKey>(Expression<Func<TRoot, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Sum<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Sum<TKey>(Expression<Func<TRoot, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Min<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Min<TKey>(Expression<Func<TRoot, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Max<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Max<TKey>(Expression<Func<TRoot, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        protected GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> ActivateSingleSelector<TAggregateNode, TKey>(Expression<Func<TAggregateNode, TKey>> keySelector, string aggregateMethod)
        {
            var selector = _graphQLExpressionConverter.Convert(keySelector);
            return ActivateSelectors(new[] { selector }, aggregateMethod);
        }

        protected GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> ActivateMultiSelector<TAggregateNode, TKey>(Expression<Func<TAggregateNode, IEnumerable<TKey>>> keySelectors, string aggregateMethod)
        {
            var selectors = _graphQLExpressionConverter.Convert(keySelectors);
            return ActivateSelectors(selectors, aggregateMethod);
        }

        private GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> ActivateSelectors(IEnumerable<IGraphQLValueStatement> selectors, string aggregateMethod)
        {
            _graphQLSelectNode.IsActive = true;

            var aggregateNode = _graphQLSelectNode.GetChildNode<IGraphQLAggregateNode>();
            var methodNode = aggregateNode.GetChildNode(aggregateMethod);
            aggregateNode.IsActive = true;
            methodNode.IsActive = true;

            foreach (var selector in selectors)
            {
                var propertyStatement = new GraphQLPropertyStatement(selector.PropertyName);
                methodNode.PropertyStatements = methodNode.PropertyStatements.Append(propertyStatement);
            }

            return this;
        }

        protected GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> Nodes()
        {
            if (_graphQLAggregateNodes is null)
                _graphQLAggregateNodes = _graphQLSelectNode.ChildSelectNodes.First(x => x.HeaderNode.Title.Equals(Constant.GraphQLKeyords.Nodes));

            _graphQLAggregateNodes.Activate();
            return this;
        }

        IGraphQLRootAggregateNodesBuilder<TRoot> IGraphQLRootAggregateBuilder<TRoot>.Nodes()
        {
            return Nodes();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Nodes()
        {
            return Nodes();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Nodes()
        {
            return Nodes();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Nodes()
        {
            return Nodes();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Nodes()
        {
            return Nodes();
        }

        private GraphQLQueryBuilder<TRoot, TEntity> End()
        {
            if (_parentBuilder is null)
                _nodeBuilders.TryGetValue(typeof(TEntity).TypeHandle, out _parentBuilder);
            
            return (GraphQLQueryBuilder<TRoot, TEntity>) _parentBuilder;
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.End()
        {
            return End();
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.End()
        {
            return End();
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.End()
        {
            return End();
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.End()
        {
            return End();
        }

        protected GraphQLChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>()
            where TChildAggregate : IGraphQLEntity
        {
            if (_childAggregateBuilders is null)
                _childAggregateBuilders = new Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder>();

            if (_graphQLAggregateNodes is null)
                _graphQLAggregateNodes = _graphQLSelectNode.ChildSelectNodes.First(x => x.HeaderNode.Title.Equals(Constant.GraphQLKeyords.Nodes));

            var key = typeof(TChildAggregate).TypeHandle;
            var success = _childAggregateBuilders.TryGetValue(key, out IGraphQLQueryBuilder builder);
            if (!success)
            {
                var childAggregateNode = _graphQLAggregateNodes.AggregateContainerNodes.FirstOrDefault(x => x.EntityType.Equals(typeof(TChildAggregate)));
                if (childAggregateNode is null)
                    throw new InvalidOperationException("Selected aggregate is not part of the current aggregate node.");

                var childAggregateBuilder = new GraphQLChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate>(
                    _graphQLQuery, childAggregateNode, _graphQLExpressionConverter, _graphQLValueFactory, this);

                _childAggregateBuilders.Add(key, childAggregateBuilder);
                builder = childAggregateBuilder;
            }

            return (GraphQLChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate>) builder;
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TAggregate, TChildAggregate> IGraphQLSingleAggregateBuilder<TRoot, TAggregate>.Aggregate<TChildAggregate>()
        {
            return Aggregate<TChildAggregate>();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>.Aggregate<TChildAggregate>()
        {
            return Aggregate<TChildAggregate>();
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TAggregate, TChildAggregate> IGraphQLStandardAggregateBuilder<TRoot, TAggregate>.Aggregate<TChildAggregate>()
        {
            return Aggregate<TChildAggregate>();
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate>.Aggregate<TChildAggregate>()
        {
            return Aggregate<TChildAggregate>();
        }

        private GraphQLMethodConstruct Build()
        {
            _graphQLSelectNode.EntityType = typeof(IGraphQLAggregateContainerNode<TRoot>);
            return new GraphQLMethodConstruct<IGraphQLAggregateContainerNode<TRoot>>(GraphQLMethod.Query, _graphQLSelectNode.HeaderNode, _graphQLSelectNode)
            {
                IsSingle = true
            };
        }

        IGraphQLSingleQuery<IGraphQLAggregateContainerNode<TRoot>> IGraphQLRootAggregateBuilder<TRoot>.Build()
        {
            return (IGraphQLSingleQuery<IGraphQLAggregateContainerNode<TRoot>>)Build();
        }

        IGraphQLSingleQuery<IGraphQLAggregateContainerNode<TRoot>> IGraphQLRootAggregateNodesBuilder<TRoot>.Build()
        {
            return (IGraphQLSingleQuery<IGraphQLAggregateContainerNode<TRoot>>)Build();
        }

        IGraphQLSingleSelectedQuery<IGraphQLAggregateContainerNode<TRoot>, IGraphQLAggregateContainerNode<TResult>> IGraphQLRootAggregateNodesBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            var expressionStatement = _graphQLExpressionConverter.ConvertSelectExpression(selector);
            expressionStatement.ApplySelectStatement(_graphQLAggregateNodes);

            var selectorFunc = selector.Compile();
            var aggregateSelectorFunc = new Func<IGraphQLAggregateContainerNode<TRoot>, IGraphQLAggregateContainerNode<TResult>>((aggregateContainer) =>
            {
                var resultAggregateContainer = new GraphQLAggregateContainerNode<TResult>(_graphQLExpressionConverter)
                {
                    Aggregate = aggregateContainer.Aggregate,
                    Nodes = new List<TResult>()
                };

                foreach(var item in aggregateContainer.Nodes)                
                    resultAggregateContainer.Nodes.Add(selectorFunc.Invoke(item));

                return resultAggregateContainer;
            });

            var query = new GraphQLSelectedMethodConstruct<
                IGraphQLAggregateContainerNode<TRoot>,
                IGraphQLAggregateContainerNode<TResult>>
                (GraphQLMethod.Query, _graphQLSelectNode.HeaderNode, _graphQLSelectNode, aggregateSelectorFunc)
            {
                IsSingle = true
            };

            return query;
        }
    }
}
