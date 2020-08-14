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
    public interface IGraphQLQueryBuilder
    {
    }

    public interface IGraphQLSingleQueryBuilder<TEntity> : IGraphQLQueryBuilder
    {
        IGraphQLSingleQuery<TEntity> Build();
    }

    public interface IGraphQLMultiQueryBuilder<TEntity> : IGraphQLQueryBuilder
    {
        IGraphQLQuery<TEntity> Build();
    }

    public interface IGraphQLSingleNodeBuilderBase<TEntity> : IGraphQLSingleQueryBuilder<TEntity>
    {
        IGraphQLSingleNodeBuilder<TEntity, TNode> Node<TNode>();
    }

    public interface IGraphQLMultiNodeBuilderBase<TEntity> : IGraphQLMultiQueryBuilder<TEntity>
    {
        IGraphQLMultiNodeBuilder<TEntity, TNode> Node<TNode>();
    }

    public interface IGraphQLSingleNodeBuilder<TEntity> : IGraphQLSingleNodeBuilderBase<TEntity>
    {
        IGraphQLSingleNodeBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLSingleNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLSingleNodeBuilder<TRoot, TNode> : IGraphQLMultiNodeBuilderBase<TRoot>
    {
        IGraphQLSingleNodeBuilder<TRoot, TNode> Where(Expression<Func<TNode, bool>> expressionPredicate);
        IGraphQLSingleNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLMultiNodeBuilder<TEntity> : IGraphQLMultiNodeBuilderBase<TEntity>
    {
        IGraphQLMultiNodeBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLMultiNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLMultiOrderedNodeBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>();
        IGraphQLSingleNodeBuilder<TEntity> Single(Expression<Func<TEntity, bool>> expressionPredicate);
    }

    public interface IGraphQLMultiNodeBuilder<TRoot, TNode> : IGraphQLMultiNodeBuilderBase<TRoot>
    {
        IGraphQLMultiNodeBuilder<TRoot, TNode> Where(Expression<Func<TNode, bool>> expressionPredicate);
        IGraphQLMultiNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> OrderBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> OrderByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> OrderByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLRootNodeBuilder<TEntity> : IGraphQLMultiNodeBuilder<TEntity>
    {
        IGraphQLSingleQueryBuilder<TEntity> ById(object value);
        IGraphQLSingleQueryBuilder<TEntity> ByPrimaryKey(string key, object value);
    }

    public interface IGraphQLSingleOrderedNodeBuilder<TEntity> : IGraphQLSingleNodeBuilderBase<TEntity>
    {
        IGraphQLSingleNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TEntity> DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> : IGraphQLMultiNodeBuilderBase<TRoot>
    {
        IGraphQLSingleNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> DistinctOn<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> ThenBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> ThenByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> ThenByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLMultiOrderedNodeBuilder<TEntity> : IGraphQLMultiNodeBuilderBase<TEntity>
    {
        IGraphQLMultiNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLMultiOrderedNodeBuilder<TEntity> DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> : IGraphQLMultiNodeBuilderBase<TRoot>
    {
        IGraphQLMultiNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> DistinctOn<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> ThenBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> ThenByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> ThenByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>();
    }

    public interface IGraphQLSingleAggregateBuilder<TEntity, TAggregate> : IGraphQLSingleQueryBuilder<TEntity>
    {
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Count();

        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Nodes();
        IGraphQLSingleNodeBuilder<TEntity> End();

        IGraphQLSingleChildAggregateBuilder<TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>();
    }

    public interface IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> : IGraphQLMultiQueryBuilder<TRoot>
    {
        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Count();

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> Nodes();
        IGraphQLSingleNodeBuilder<TRoot, TEntity> End();

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>();
    }

    public interface IGraphQLMultiAggregateBuilder<TEntity, TAggregate> : IGraphQLMultiQueryBuilder<TEntity>
    {
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Count();

        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TEntity, TAggregate> Nodes();
        IGraphQLMultiNodeBuilder<TEntity> End();

        IGraphQLMultiChildAggregateBuilder<TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>();
    }

    public interface IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> : IGraphQLMultiQueryBuilder<TRoot>
    {
        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Count();

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> Nodes();
        IGraphQLMultiNodeBuilder<TRoot, TEntity> End();

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>();
    }

    public interface IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> : IGraphQLSingleQueryBuilder<TRoot>
    {
        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Count();

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> Nodes();
        IGraphQLSingleAggregateBuilder<TRoot, TParent> End();
    }

    public interface IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> : IGraphQLMultiQueryBuilder<TRoot>
    {
        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Count();

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Nodes();
        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TParent> End();
    }

    public interface IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> : IGraphQLMultiQueryBuilder<TRoot>
    {
        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Count();

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TParent, TAggregate> Nodes();
        IGraphQLMultiAggregateBuilder<TRoot, TParent> End();
    }

    public interface IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> : IGraphQLMultiQueryBuilder<TRoot>
    {
        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Count();

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLMultiChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Nodes();
        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TParent> End();
    }
}
