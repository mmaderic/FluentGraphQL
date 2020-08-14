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
using FluentGraphQL.Builder.Constants;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Builders
{
    public class GraphQLChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> : GraphQLAggregateBuilder<TRoot, TEntity, TAggregate>,
        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>, IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>,
        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>, IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>
    {
        private readonly IGraphQLQueryBuilder _parentBuilder;

        public GraphQLChildAggregateBuilder(
            IGraphQLQuery graphQLQuery, IGraphQLSelectNode graphQLSelectNode, 
            IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory,  IGraphQLQueryBuilder parentBuilder) 
            : base(graphQLQuery, graphQLSelectNode, graphQLExpressionConverter, graphQLValuefactory)
        {
            _parentBuilder = parentBuilder;
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Count()
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) Count();
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Count()
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Count();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Count()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) Count();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Count()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Count();
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder < TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max); 
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate >) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate >) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>) Nodes();
        }

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Nodes();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) Nodes();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Nodes();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TParent> IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate>.End()
        {
            return (IGraphQLMultiAggregateBuilder<TRoot, TParent>) _parentBuilder;
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TParent> IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.End()
        {
            return (IGraphQLMultiAggregateBuilder<TRoot, TEntity, TParent>) _parentBuilder;
        }

        IGraphQLSingleAggregateBuilder<TRoot, TParent> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.End()
        {
            return (IGraphQLSingleAggregateBuilder<TRoot, TParent>) _parentBuilder;
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TParent> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.End()
        {
            return (IGraphQLSingleAggregateBuilder<TRoot, TEntity, TParent>) _parentBuilder;
        }
    }
}
