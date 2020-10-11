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
        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>, IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
        where TParent : IGraphQLEntity
        where TAggregate : IGraphQLEntity
    {
        private readonly IGraphQLQueryBuilder _parentBuilder;

        public GraphQLChildAggregateBuilder(
            IGraphQLNodeConstruct graphQLNodeConstruct, IGraphQLSelectNode graphQLSelectNode, 
            IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory,  IGraphQLQueryBuilder parentBuilder) 
            : base(graphQLNodeConstruct, graphQLSelectNode, graphQLExpressionConverter, graphQLValuefactory)
        {
            _parentBuilder = parentBuilder;
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Count()
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) Count();
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Count()
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Count();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Count()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) Count();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Count()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Count();
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Avg);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Avg);
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

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Sum);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Sum);
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

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Min);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Min);
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

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateSingleSelector(keySelector, Constant.AggregateMethodCalls.Max);
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors)
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) ActivateMultiSelector(keySelectors, Constant.AggregateMethodCalls.Max);
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

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>) Nodes();
        }

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Nodes();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate>) Nodes();
        }

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.Nodes()
        {
            return (IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>) Nodes();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TParent> IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate>.End()
        {
            return (IGraphQLStandardAggregateBuilder<TRoot, TParent>) _parentBuilder;
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TParent> IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate>.End()
        {
            return (IGraphQLStandardAggregateBuilder<TRoot, TEntity, TParent>) _parentBuilder;
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
