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

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLBuilderFactory
    {
        IGraphQLRootNodeBuilder<TEntity> QueryBuilder<TEntity>()
            where TEntity : IGraphQLEntity;

        IGraphQLMutationBuilder<TEntity> MutationBuilder<TEntity>()
            where TEntity : IGraphQLEntity;

        IGraphQLFunctionQueryBuilder<TEntity> FunctionBuilder<TEntity>(IGraphQLFunction<TEntity> graphQLFunction)
            where TEntity : IGraphQLEntity;

        IGraphQLActionBuilder ActionBuilder();
    }
}
