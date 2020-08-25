﻿/*
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

namespace FluentGraphQL.Builder.Constructs
{
    public class GraphQLSelectedMutation<TEntity, TResult> : GraphQLMutation<TEntity>,
        IGraphQLSelectedInsertSingleMutation<TEntity, TResult>, IGraphQLSelectedInsertMultipleMutation<TEntity, TResult>
           where TEntity : IGraphQLEntity
    {
        public Func<TEntity, TResult> Selector { get; set; }
        public bool IsSelected { get; set; }

        public GraphQLSelectedMutation(IGraphQLHeaderNode graphQLHeaderNode, IGraphQLSelectNode graphQLSelectNode, Func<TEntity, TResult> selector)
            : base(graphQLHeaderNode, graphQLSelectNode)
        {
            Selector = selector;
            IsSelected = true;
        }

        IGraphQLInsertSingleMutation<TEntity> IGraphQLSelectedInsertSingleMutation<TEntity, TResult>.AsNamed()
        {
            IsSelected = false;
            return this;
        }

        IGraphQLInsertMultipleMutation<TEntity> IGraphQLSelectedInsertMultipleMutation<TEntity, TResult>.AsNamed()
        {
            IsSelected = false;
            return this;
        }

        public object InvokeSelector(object @object)
        {
            if (Selector is null)
                return null;

            return Selector.Invoke((TEntity)@object);
        }
    }
}