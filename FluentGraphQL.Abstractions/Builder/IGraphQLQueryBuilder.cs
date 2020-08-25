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
        where TEntity : IGraphQLEntity
    {
        IGraphQLSingleQuery<TEntity> Build();
    }

    public interface IGraphQLStandardQueryBuilderBase<TEntity> : IGraphQLQueryBuilder
        where TEntity : IGraphQLEntity
    {
        IGraphQLStandardQuery<TEntity> Build();
    }

    public interface IGraphQLStandardQueryBuilder<TEntity> : IGraphQLStandardQueryBuilderBase<TEntity>
       where TEntity : IGraphQLEntity
    {
        IGraphQLSingleNodeBuilder<TEntity> Single(Expression<Func<TEntity, bool>> expressionPredicate = null);
    }

    public interface IGraphQLSingleNodeBuilderBase<TEntity> : IGraphQLSingleQueryBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLSingleNodeBuilder<TEntity, TNode> Node<TNode>() where TNode : IGraphQLEntity;
    }

    public interface IGraphQLStandardNodeBuilderBase<TEntity> : IGraphQLStandardQueryBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLStandardNodeBuilder<TEntity, TNode> Node<TNode>() where TNode : IGraphQLEntity;
    }

    public interface IGraphQLSingleNodeBuilder<TEntity> : IGraphQLSingleNodeBuilderBase<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLSingleNodeBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLSingleNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
        IGraphQLSingleSelectedQuery<TEntity, TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);
    }

    public interface IGraphQLSingleNodeBuilder<TRoot, TNode> : IGraphQLStandardNodeBuilderBase<TRoot>
        where TRoot : IGraphQLEntity
        where TNode : IGraphQLEntity
    {
        IGraphQLSingleNodeBuilder<TRoot, TNode> Where(Expression<Func<TNode, bool>> expressionPredicate);
        IGraphQLSingleNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
        IGraphQLSingleSelectedQuery<TRoot, TResult> Select<TResult>(Expression<Func<TRoot, TResult>> selector);
    }

    public interface IGraphQLStandardNodeBuilder<TEntity> : IGraphQLStandardNodeBuilderBase<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLStandardNodeBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expressionPredicate);
        IGraphQLStandardNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLStandardOrderedNodeBuilder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
        IGraphQLStandardSelectedQuery<TEntity, TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);
    }

    public interface IGraphQLStandardNodeBuilder<TRoot, TNode> : IGraphQLStandardNodeBuilderBase<TRoot>
        where TRoot : IGraphQLEntity
        where TNode : IGraphQLEntity
    {
        IGraphQLStandardNodeBuilder<TRoot, TNode> Where(Expression<Func<TNode, bool>> expressionPredicate);
        IGraphQLStandardNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> OrderBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> OrderByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> OrderByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
        IGraphQLStandardSelectedQuery<TRoot, TResult> Select<TResult>(Expression<Func<TRoot, TResult>> selector);
    }

    public interface IGraphQLRootNodeBuilder<TEntity> : IGraphQLStandardNodeBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLSingleQueryBuilder<TEntity> ById(object value);
        IGraphQLSingleQueryBuilder<TEntity> ByPrimaryKey(string key, object value);
        IGraphQLSingleQueryBuilder<TEntity> ByPrimaryKey<TPrimaryKey>(Expression<Func<TEntity, TPrimaryKey>> propertyExpression, TPrimaryKey value);
    }

    public interface IGraphQLSingleOrderedNodeBuilder<TEntity> : IGraphQLSingleNodeBuilderBase<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLSingleNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TEntity> DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
    }

    public interface IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> : IGraphQLStandardNodeBuilderBase<TRoot>
        where TRoot : IGraphQLEntity
        where TNode : IGraphQLEntity
    {
        IGraphQLSingleNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> DistinctOn<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> ThenBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> ThenByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> ThenByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLSingleAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
    }

    public interface IGraphQLStandardOrderedNodeBuilder<TEntity> : IGraphQLStandardNodeBuilderBase<TEntity>
        where TEntity : IGraphQLEntity
    {
        IGraphQLStandardNodeBuilder<TEntity> Limit(int number, int offset = 0);
        IGraphQLStandardOrderedNodeBuilder<TEntity> DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TEntity> OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
    }

    public interface IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> : IGraphQLStandardNodeBuilderBase<TRoot>
        where TRoot : IGraphQLEntity
        where TNode : IGraphQLEntity
    {
        IGraphQLStandardNodeBuilder<TRoot, TNode> Limit(int number, int offset = 0);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> DistinctOn<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> ThenBy<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> ThenByDescending<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> ThenByNullsFirst<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardOrderedNodeBuilder<TRoot, TNode> OrderByDescendingNullsLast<TKey>(Expression<Func<TNode, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TRoot, TNode, TAggregate> Aggregate<TAggregate>() where TAggregate : IGraphQLEntity;
    }

    public interface IGraphQLSingleAggregateBuilder<TEntity, TAggregate> : IGraphQLSingleQueryBuilder<TEntity>
        where TEntity : IGraphQLEntity
        where TAggregate : IGraphQLEntity
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

        IGraphQLSingleChildAggregateBuilder<TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>() where TChildAggregate : IGraphQLEntity;
    }

    public interface IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> : IGraphQLStandardQueryBuilder<TRoot>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
        where TAggregate : IGraphQLEntity
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

        IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>() where TChildAggregate : IGraphQLEntity;
    }

    public interface IGraphQLStandardAggregateBuilder<TEntity, TAggregate> : IGraphQLStandardQueryBuilder<TEntity>
        where TEntity : IGraphQLEntity
        where TAggregate : IGraphQLEntity
    {
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Count();

        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TEntity, TAggregate> Nodes();
        IGraphQLStandardNodeBuilder<TEntity> End();

        IGraphQLStandardChildAggregateBuilder<TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>() where TChildAggregate : IGraphQLEntity;
    }

    public interface IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> : IGraphQLStandardQueryBuilder<TRoot>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
        where TAggregate : IGraphQLEntity
    {
        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Count();

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> Nodes();
        IGraphQLStandardNodeBuilder<TRoot, TEntity> End();

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TAggregate, TChildAggregate> Aggregate<TChildAggregate>() where TChildAggregate : IGraphQLEntity;
    }

    public interface IGraphQLSingleChildAggregateBuilder<TRoot, TParent, TAggregate> : IGraphQLSingleQueryBuilder<TRoot>
        where TRoot : IGraphQLEntity
        where TParent : IGraphQLEntity
        where TAggregate : IGraphQLEntity
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

    public interface IGraphQLSingleChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> : IGraphQLStandardQueryBuilder<TRoot>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
        where TParent : IGraphQLEntity
        where TAggregate : IGraphQLEntity
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

    public interface IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> : IGraphQLStandardQueryBuilder<TRoot>
        where TRoot : IGraphQLEntity
        where TParent : IGraphQLEntity
        where TAggregate : IGraphQLEntity
    {
        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Count();

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TParent, TAggregate> Nodes();
        IGraphQLStandardAggregateBuilder<TRoot, TParent> End();
    }

    public interface IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> : IGraphQLStandardQueryBuilder<TRoot>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
        where TParent : IGraphQLEntity
        where TAggregate : IGraphQLEntity
    {
        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Count();

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Sum<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Avg<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Max<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, TKey>> keySelector);
        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Min<TKey>(Expression<Func<TAggregate, IEnumerable<TKey>>> keySelectors);

        IGraphQLStandardChildAggregateBuilder<TRoot, TEntity, TParent, TAggregate> Nodes();
        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TParent> End();
    }
}
