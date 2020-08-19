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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Builders
{
    public class GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> : GraphQLQueryBuilder<TRoot, TEntity>,
        IGraphQLSingleAggregateBuilder<TRoot, TAggregate>, IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate>,
        IGraphQLMultiAggregateBuilder<TRoot, TAggregate>, IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>
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

            var aggregateNode = _graphQLSelectNode.GetChildNode<IGraphQLAggregate>();
            aggregateNode.IsActive = true;
            aggregateNode.ActivateProperty(Constant.AggregateMethodCalls.Count);

            return this;
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Count()
        {
            return Count();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Count()
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

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
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

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
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

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
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

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
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

        protected GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> ActivateSingleSelector<TKey>(Expression<Func<TAggregate, TKey>> keySelector, string aggregateMethod)
        {
            var selector = _graphQLExpressionConverter.Convert(keySelector);
            return ActivateSelectors(new[] { selector }, aggregateMethod);
        }

        protected GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> ActivateMultiSelector<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors, string aggregateMethod)
        {
            var selectors = _graphQLExpressionConverter.Convert(keySelectors);
            return ActivateSelectors(selectors, aggregateMethod);
        }

        private GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> ActivateSelectors(IEnumerable<IGraphQLValueStatement> selectors, string aggregateMethod)
        {
            _graphQLSelectNode.IsActive = true;

            var aggregateNode = _graphQLSelectNode.GetChildNode<IGraphQLAggregate>();
            var methodNode = aggregateNode.GetChildNode(aggregateMethod);
            aggregateNode.IsActive = true;
            methodNode.IsActive = true;

            foreach (var selector in selectors)
            {
                var propertyStatement = new GraphQLPropertyStatement(((IGraphQLPropertyValue)selector.Value).ValueLiteral);
                ((List<IGraphQLPropertyStatement>)methodNode.PropertyStatements).Add(propertyStatement);
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

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Nodes()
        {
            return Nodes();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Nodes()
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

        IGraphQLMultiNodeBuilder<TRoot> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.End()
        {
            return End();
        }

        IGraphQLMultiNodeBuilder<TRoot, TEntity> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.End()
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

        IGraphQLMultiChildAggregateBuilder<TRoot, TAggregate, TChildAggregate> IGraphQLMultiAggregateBuilder<TRoot, TAggregate>.Aggregate<TChildAggregate>()
        {
            return Aggregate<TChildAggregate>();
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate>.Aggregate<TChildAggregate>()
        {
            return Aggregate<TChildAggregate>();
        }
    }
}
