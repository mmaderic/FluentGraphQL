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
using FluentGraphQL.Builder.Atoms;
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Builders
{  
    public class GraphQLQueryBuilder<TRoot, TEntity> :
        IGraphQLRootNodeBuilder<TRoot>, 
        IGraphQLSingleNodeBuilder<TRoot>, IGraphQLSingleNodeBuilder<TRoot, TEntity>,
        IGraphQLMultiNodeBuilder<TRoot>, IGraphQLMultiNodeBuilder<TRoot, TEntity>,
        IGraphQLSingleOrderedNodeBuilder<TRoot>, IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>,
        IGraphQLMultiOrderedNodeBuilder<TRoot>, IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>
    {
        protected readonly IGraphQLQuery _graphQLQuery;
        protected readonly IGraphQLSelectNode _graphQLSelectNode;
        protected readonly IGraphQLExpressionConverter _graphQLExpressionConverter;
        protected readonly IGraphQLValueFactory _graphQLValueFactory;

        protected Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder> _nodeBuilders;
        private Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder> _aggregateBuilders;

        public GraphQLQueryBuilder(
            IGraphQLQuery graphQLQuery, IGraphQLSelectNode graphQLSelectNode,
            IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory)
        {
            _graphQLQuery = graphQLQuery;
            _graphQLSelectNode = graphQLSelectNode;
            _graphQLExpressionConverter = graphQLExpressionConverter;
            _graphQLValueFactory = graphQLValuefactory;
        }     

        private GraphQLQueryBuilder<TRoot, TEntity> ByPrimaryKey(string key, object value)
        {
            var graphQLValue = _graphQLValueFactory.Construct(value);
            _graphQLSelectNode.HeaderNode.Suffix = Constant.GraphQLKeyords.ByPk;
            _graphQLSelectNode.HeaderNode.Statements.Add(new GraphQLValueStatement(key, graphQLValue));

            return this;
        }

        IGraphQLSingleQueryBuilder<TRoot> IGraphQLRootNodeBuilder<TRoot>.ById(object value)
        {
            return ByPrimaryKey(Constant.GraphQLKeyords.Id, value);
        }

        IGraphQLSingleQueryBuilder<TRoot> IGraphQLRootNodeBuilder<TRoot>.ByPrimaryKey(string key, object value)
        {
            return ByPrimaryKey(key, value);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> Where<TNode>(Expression<Func<TNode, bool>> expressionPredicate)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(expressionPredicate);
            var expressionStatementObject = new GraphQLObjectValue(expressionStatement);
            var whereStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Where, expressionStatementObject);

            _graphQLSelectNode.HeaderNode.Statements.Add(whereStatement);

            return this;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.Where(Expression<Func<TRoot, bool>> expressionPredicate)
        {
            return Where(expressionPredicate);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Where(Expression<Func<TEntity, bool>> expressionPredicate)
        {
            return Where(expressionPredicate);
        }

        IGraphQLMultiNodeBuilder<TRoot> IGraphQLMultiNodeBuilder<TRoot>.Where(Expression<Func<TRoot, bool>> expressionPredicate)
        {
            return Where(expressionPredicate);
        }

        IGraphQLMultiNodeBuilder<TRoot, TEntity> IGraphQLMultiNodeBuilder<TRoot, TEntity>.Where(Expression<Func<TEntity, bool>> expressionPredicate)
        {
            return Where(expressionPredicate);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> Limit(int number, int offset)
        {
            var limitStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Limit, _graphQLValueFactory.Construct(number));
            var offsetStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Offset, _graphQLValueFactory.Construct(offset));

            _graphQLSelectNode.HeaderNode.Statements.Add(limitStatement);
            _graphQLSelectNode.HeaderNode.Statements.Add(offsetStatement);

            return this;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLMultiNodeBuilder<TRoot> IGraphQLMultiNodeBuilder<TRoot>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLMultiNodeBuilder<TRoot, TEntity> IGraphQLMultiNodeBuilder<TRoot, TEntity>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLMultiNodeBuilder<TRoot> IGraphQLMultiOrderedNodeBuilder<TRoot>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLMultiNodeBuilder<TRoot, TEntity> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> ResolveOrderByStatement<TNode, TKey>(Expression<Func<TNode, TKey>> keySelector, OrderByDirection orderByDirection)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(keySelector, orderByDirection);
            var expressionStatementObject = new GraphQLObjectValue(expressionStatement);
            var orderByStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.OrderBy, expressionStatementObject);

            var existingOrderByStatement = _graphQLSelectNode.HeaderNode.Statements.Find<GraphQLValueStatement>(Constant.GraphQLKeyords.OrderBy);
            if (existingOrderByStatement is null)
            {
                _graphQLSelectNode.HeaderNode.Statements.Add(orderByStatement);
                return this;
            }

            if (existingOrderByStatement.Value is GraphQLObjectValue objectValue)
                existingOrderByStatement.Value = new GraphQLCollectionValue(new List<GraphQLObjectValue> { objectValue, expressionStatementObject });
            else
            {
                var collectionValue = existingOrderByStatement.Value as GraphQLCollectionValue;
                var items = collectionValue.CollectionItems as List<GraphQLObjectValue>;
                items.Add(expressionStatementObject);
            }

            return this;
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiNodeBuilder<TRoot>.OrderBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiNodeBuilder<TRoot, TEntity>.OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiNodeBuilder<TRoot>.OrderByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiNodeBuilder<TRoot, TEntity>.OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiNodeBuilder<TRoot>.OrderByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiNodeBuilder<TRoot>.OrderByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiNodeBuilder<TRoot, TEntity>.OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> DistinctOn<TNode, TKey>(Expression<Func<TNode, TKey>> keySelector)
        {
            var distinctSelector = _graphQLExpressionConverter.Convert(keySelector);
            var distinctStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.DistinctOn, distinctSelector.Value);
            var orderByStatement = _graphQLSelectNode.HeaderNode.Statements.Find<GraphQLValueStatement>(Constant.GraphQLKeyords.OrderBy);
            var oderByIndex = _graphQLSelectNode.HeaderNode.Statements.IndexOf(orderByStatement);

            _graphQLSelectNode.HeaderNode.Statements.Insert(oderByIndex, distinctStatement);

            return this;
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiOrderedNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderBy(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>)this).OrderBy(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiOrderedNodeBuilder<TRoot>.ThenBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot>) this).OrderBy(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot, TEntity>)this).OrderBy(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderByDescending(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>) this).OrderByDescending(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiOrderedNodeBuilder<TRoot>.ThenByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot>)this).OrderByDescending(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot, TEntity>)this).OrderByDescending(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiOrderedNodeBuilder<TRoot>.ThenByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot, TEntity>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.OrderByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderByDescendingNullsLast(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot> IGraphQLMultiOrderedNodeBuilder<TRoot>.OrderByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot>)this).OrderByDescendingNullsLast(keySelector);
        }

        IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLMultiNodeBuilder<TRoot, TEntity>)this).OrderByDescendingNullsLast(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>)this).OrderByDescendingNullsLast(keySelector);
        }

        private GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> Aggregate<TAggregate>()
        {
            if (_aggregateBuilders is null)
                _aggregateBuilders = new Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder>();

            if (_nodeBuilders is null)
            {
                _nodeBuilders = new Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder>
                {
                    { typeof(TEntity).TypeHandle, this }
                };
            }

            var key = typeof(TAggregate).TypeHandle;
            var success = _aggregateBuilders.TryGetValue(key, out IGraphQLQueryBuilder builder);
            if (!success)
            {
                var aggregateNode = _graphQLSelectNode.AggregateContainerNodes.FirstOrDefault(x => x.EntityType.Equals(typeof(TAggregate)));
                if (aggregateNode is null)
                    throw new InvalidOperationException("Selected aggregate is not part of the current query node.");

                var aggregateBuilder = new GraphQLAggregateBuilder<TRoot, TEntity, TAggregate>(_graphQLQuery, aggregateNode, _graphQLExpressionConverter, _graphQLValueFactory)
                {
                    _nodeBuilders = _nodeBuilders
                };

                _aggregateBuilders.Add(key, aggregateBuilder);
                builder = aggregateBuilder;
            }

            return (GraphQLAggregateBuilder<TRoot, TEntity, TAggregate>)builder;
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleOrderedNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TAggregate> IGraphQLMultiOrderedNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLMultiAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLMultiOrderedNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        private GraphQLQueryBuilder<TRoot, TNode> Node<TNode>()
        {
            if (_nodeBuilders is null)
            {
                _nodeBuilders = new Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder>
                {
                    { typeof(TEntity).TypeHandle, this }
                };
            }

            var key = typeof(TNode).TypeHandle;
            var success = _nodeBuilders.TryGetValue(key, out IGraphQLQueryBuilder builder);
            if (!success)
            {
                var node = _graphQLQuery.GetSelectNode<TNode>();
                if (node is null)
                    throw new InvalidOperationException("Selected node is not part of the query.");

                var queryBuilder = new GraphQLQueryBuilder<TRoot, TNode>(_graphQLQuery, node, _graphQLExpressionConverter, _graphQLValueFactory)
                {
                    _nodeBuilders = _nodeBuilders
                };

                _nodeBuilders.Add(key, queryBuilder);
                builder = queryBuilder;
            }

            return (GraphQLQueryBuilder<TRoot, TNode>) builder;
        }

        IGraphQLSingleNodeBuilder<TRoot, TNode> IGraphQLSingleNodeBuilderBase<TRoot>.Node<TNode>()
        {
            return Node<TNode>();
        }

        IGraphQLMultiNodeBuilder<TRoot, TNode> IGraphQLMultiNodeBuilderBase<TRoot>.Node<TNode>()
        {
            return Node<TNode>();
        }

        IGraphQLQuery<TRoot> IGraphQLMultiQueryBuilder<TRoot>.Build()
        {
            return (IGraphQLQuery<TRoot>)_graphQLQuery;
        }

        IGraphQLSingleQuery<TRoot> IGraphQLSingleQueryBuilder<TRoot>.Build()
        {
            return (IGraphQLSingleQuery<TRoot>)_graphQLQuery;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLMultiQueryBuilder<TRoot>.Single(Expression<Func<TRoot, bool>> expressionPredicate)
        {
            if (expressionPredicate is null)
                return this;
            
            Node<TRoot>().Where(expressionPredicate);

            return this;            
        }
    }
}
