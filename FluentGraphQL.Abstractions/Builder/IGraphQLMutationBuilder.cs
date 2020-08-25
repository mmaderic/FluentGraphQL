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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLMutationBuilder
    {
    }

    public interface IGraphQLMutationBuilder<TEntity> : IGraphQLMutationBuilder
        where TEntity : IGraphQLEntity
    {
        IGraphQLInsertSingleMutationBuilder<TEntity> Insert(TEntity entity);
        IGraphQLInsertMultipleMutationBuilder<TEntity> Insert(IEnumerable<TEntity> entities);
    }

    public interface IGraphQLInsertSingleMutationBuilder<TEntity> : IGraphQLMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLSelectedInsertSingleMutation<TEntity, TReturn> Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression);
        IGraphQLInsertSingleMutation<TEntity> Build();
    }

    public interface IGraphQLInsertMultipleMutationBuilder<TEntity> : IGraphQLMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLSelectedInsertMultipleMutation<TEntity, TReturn> Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression);
        IGraphQLInsertMultipleMutation<TEntity> Build();
    }
}