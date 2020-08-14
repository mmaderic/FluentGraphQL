using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLAggregateContainer
    {
        IGraphQLAggregate Aggregate { get; set; }
    }

    public interface IGraphQLAggregateContainer<TEntity> : IGraphQLAggregateContainer
    {
        ICollection<TEntity> Nodes { get; set; }

        int Count();
        TKey Max<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        TKey Avg<TKey>(Expression<Func<TEntity, TKey>> keySelector);
    }
}
