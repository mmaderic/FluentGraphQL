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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Constructs
{
    public class GraphQLAggregateContainer<TEntity> : IGraphQLAggregateContainer<TEntity>
    {
        private readonly IGraphQLExpressionConverter _graphQLExpressionConverter;

        public ICollection<TEntity> Nodes { get; set; }
        public IGraphQLAggregate Aggregate { get; set; }

        public GraphQLAggregateContainer(IGraphQLExpressionConverter graphQLExpressionConverter)
        {
            _graphQLExpressionConverter = graphQLExpressionConverter;
        }

        public int Count()
        {
            return Aggregate.Count;
        }

        public TKey Avg<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            return (TKey)Aggregate.Avg.PropertyValues[statement.Value.ToString()];
        }

        public TKey Sum<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            return (TKey)Aggregate.Sum.PropertyValues[statement.Value.ToString()];
        }

        public TKey Min<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            return (TKey)Aggregate.Min.PropertyValues[statement.Value.ToString()];
        }

        public TKey Max<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            return (TKey) Aggregate.Max.PropertyValues[statement.Value.ToString()];
        }
    }
}
