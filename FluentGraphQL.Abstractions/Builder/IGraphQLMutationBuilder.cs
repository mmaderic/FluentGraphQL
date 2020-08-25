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

        IGraphQLUpdateSingleMutationBuilder<TEntity> UpdateById(object idValue);
        IGraphQLUpdateSingleMutationBuilder<TEntity> UpdateByPrimaryKey(string key, object primaryKeyValue);
        IGraphQLUpdateSingleMutationBuilder<TEntity> UpdateByPrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> primaryKeySelector, TProperty primaryKeyValue);
        IGraphQLUpdateMultipleMutationBuilder<TEntity> UpdateWhere(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLUpdateMultipleMutationBuilder<TEntity> UpdateAll();

        IGraphQLReturnSingleMutationBuilder<TEntity> DeleteById(object idValue);
        IGraphQLReturnSingleMutationBuilder<TEntity> DeleteByPrimaryKey(string key, object primaryKeyValue);
        IGraphQLReturnSingleMutationBuilder<TEntity> DeleteByPrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> primaryKeySelector, TProperty primaryKeyValue);
        IGraphQLReturnMultipleMutationBuilder<TEntity> DeleteWhere(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLReturnMultipleMutationBuilder<TEntity> DeleteAll();
    }

    public interface IGraphQLReturnSingleMutationBuilder<TEntity>
    {
        IGraphQLSelectedReturnSingleMutation<TEntity, TReturn> Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression);
        IGraphQLReturnSingleMutation<TEntity> Build();
    }

    public interface IGraphQLReturnMultipleMutationBuilder<TEntity>
    {
        IGraphQLSelectedReturnMultipleMutation<TEntity, TReturn> Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression);
        IGraphQLReturnMultipleMutation<TEntity> Build();
    }

    public interface IGraphQLInsertSingleMutationBuilder<TEntity> : IGraphQLReturnSingleMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
    }

    public interface IGraphQLInsertMultipleMutationBuilder<TEntity> : IGraphQLReturnMultipleMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {       
    }

    public interface IGraphQLUpdateSingleMutationBuilder<TEntity> : IGraphQLReturnSingleMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLUpdateSingleMutationBuilder<TEntity> Set<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);
        IGraphQLUpdateSingleMutationBuilder<TEntity> Set(TEntity entity);

        IGraphQLUpdateSingleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, int>> propertyExpression, int incrementBy = 1);
        IGraphQLUpdateSingleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, long>> propertyExpression, long incrementBy = 1);
        IGraphQLUpdateSingleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, int?>> propertyExpression, int incrementBy = 1);
        IGraphQLUpdateSingleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, long?>> propertyExpression, long incrementBy = 1);
    }

    public interface IGraphQLUpdateMultipleMutationBuilder<TEntity> : IGraphQLReturnMultipleMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLUpdateMultipleMutationBuilder<TEntity> Set<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);

        IGraphQLUpdateMultipleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, int>> propertyExpression, int incrementBy = 1);
        IGraphQLUpdateMultipleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, long>> propertyExpression, long incrementBy = 1);
        IGraphQLUpdateMultipleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, int?>> propertyExpression, int incrementBy = 1);
        IGraphQLUpdateMultipleMutationBuilder<TEntity> Increment(Expression<Func<TEntity, long?>> propertyExpression, long incrementBy = 1);
    }
}
