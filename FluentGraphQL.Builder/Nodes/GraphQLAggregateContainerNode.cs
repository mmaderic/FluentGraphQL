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

namespace FluentGraphQL.Builder.Nodes
{
    public class GraphQLAggregateContainerNode<TEntity> : IGraphQLAggregateContainerNode<TEntity>
    {
        private readonly IGraphQLExpressionConverter _graphQLExpressionConverter;

        public ICollection<TEntity> Nodes { get; set; }
        public IGraphQLAggregateNode Aggregate { get; set; }

        public GraphQLAggregateContainerNode(IGraphQLExpressionConverter graphQLExpressionConverter)
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
            var result = Aggregate.Avg.PropertyValues[statement.PropertyName.ToString()];
            if (result is null)
                return default;

            return (TKey)result;
        }

        public TKey Sum<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            var result = Aggregate.Sum.PropertyValues[statement.PropertyName.ToString()];
            if (result is null)
                return default;

            return (TKey)result;
        }

        public TKey Min<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            var result = Aggregate.Min.PropertyValues[statement.PropertyName.ToString()];
            if (result is null)
                return default;

            return (TKey)result;
        }

        public TKey Max<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            var statement = _graphQLExpressionConverter.Convert(keySelector);
            var result = Aggregate.Max.PropertyValues[statement.PropertyName.ToString()];
            if (result is null)
                return default;

            return (TKey)result;
        }
    }
}
