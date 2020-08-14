using FluentGraphQL.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLExpressionConverter
    {
        IGraphQLValueStatement Convert<TEntity>(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLValueStatement Convert<TEntity, TKey>(Expression<Func<TEntity, TKey>> keySelector, OrderByDirection orderByDirection);
        IGraphQLValueStatement Convert<TEntity, TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IEnumerable<IGraphQLValueStatement> Convert<TEntity, TKey>(Expression<Func<TEntity, IEnumerable<TKey>>> keySelectors);
    }
}
