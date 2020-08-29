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
using System;

namespace FluentGraphQL.Builder.Constructs
{
    internal class GraphQLSelectedMethodConstruct<TEntity, TResult> : GraphQLMethodConstruct<TEntity>, 
        IGraphQLStandardSelectedQuery<TEntity, TResult>, IGraphQLSingleSelectedQuery<TEntity, TResult>,
        IGraphQLSelectedReturnSingleMutation<TEntity, TResult>, IGraphQLSelectedReturnMultipleMutation<TEntity, TResult>
           where TEntity : IGraphQLEntity
    {
        public Func<TEntity, TResult> Selector { get; set; }
        public bool IsSelected { get; set; }

        public GraphQLSelectedMethodConstruct(GraphQLMethod graphQLMethod, IGraphQLHeaderNode graphQLHeaderNode, IGraphQLSelectNode graphQLSelectNode, Func<TEntity, TResult> selector)
            : base(graphQLMethod, graphQLHeaderNode, graphQLSelectNode)
        {
            Selector = selector;
            IsSelected = true;
        }

        private GraphQLSelectedMethodConstruct<TEntity, TResult> AsNamed()
        {
            IsSelected = false;
            return this;
        }

        IGraphQLStandardQuery<TEntity> IGraphQLStandardSelectedQuery<TEntity, TResult>.AsNamed()
        {
            return AsNamed();
        }

        IGraphQLSingleQuery<TEntity> IGraphQLSingleSelectedQuery<TEntity, TResult>.AsNamed()
        {
            return AsNamed();
        }

        IGraphQLReturnSingleMutation<TEntity> IGraphQLSelectedReturnSingleMutation<TEntity, TResult>.AsNamed()
        {
            return AsNamed();
        }

        IGraphQLReturnMultipleMutation<TEntity> IGraphQLSelectedReturnMultipleMutation<TEntity, TResult>.AsNamed()
        {
            return AsNamed();
        }

        public object InvokeSelector(object @object)
        {
            if (Selector is null || @object is null)
                return null;

            return Selector.Invoke((TEntity)@object);
        }
    }
}
