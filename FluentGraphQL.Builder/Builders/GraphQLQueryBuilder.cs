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
using FluentGraphQL.Builder.Constructs;
using FluentGraphQL.Builder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentGraphQL.Builder.Builders
{
    public class GraphQLQueryBuilder<TRoot, TEntity> :
        IGraphQLRootNodeBuilder<TRoot>, IGraphQLFunctionQueryBuilder<TRoot>,
        IGraphQLSingleNodeBuilder<TRoot>, IGraphQLSingleNodeBuilder<TRoot, TEntity>,
        IGraphQLStandardNodeBuilder<TRoot>, IGraphQLStandardNodeBuilder<TRoot, TEntity>,
        IGraphQLSingleOrderedNodeBuilder<TRoot>, IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>,
        IGraphQLStandardOrderedNodeBuilder<TRoot>, IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>
        where TRoot : IGraphQLEntity
        where TEntity : IGraphQLEntity
    {
        protected readonly IGraphQLQuery _graphQLQuery;
        protected readonly IGraphQLSelectNode _graphQLSelectNode;
        protected readonly IGraphQLExpressionConverter _graphQLExpressionConverter;
        protected readonly IGraphQLValueFactory _graphQLValueFactory;
        protected readonly IGraphQLSelectNodeFactory _graphQLSelectNodeFactory;

        protected Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder> _nodeBuilders;
        private Dictionary<RuntimeTypeHandle, IGraphQLQueryBuilder> _aggregateBuilders;

        public GraphQLQueryBuilder(
            IGraphQLSelectNodeFactory graphQLSelectNodeFactory, IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValueFactory)
            : this(graphQLExpressionConverter, graphQLValueFactory)
        {
            var selectNode = graphQLSelectNodeFactory.Construct(typeof(TEntity));
            _graphQLSelectNodeFactory = graphQLSelectNodeFactory;
            _graphQLQuery = new GraphQLMethodConstruct<TEntity>(GraphQLMethod.Query, selectNode.HeaderNode, selectNode);
            _graphQLSelectNode = selectNode;
        }

        public GraphQLQueryBuilder(
            IGraphQLQuery graphQLQuery, IGraphQLSelectNode graphQLSelectNode,
            IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory)
            : this(graphQLExpressionConverter, graphQLValuefactory)
        {
            _graphQLQuery = graphQLQuery;
            _graphQLSelectNode = graphQLSelectNode;
        } 
        
        private GraphQLQueryBuilder(IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory)
        {
            _graphQLExpressionConverter = graphQLExpressionConverter;
            _graphQLValueFactory = graphQLValuefactory;
        }

        public GraphQLQueryBuilder<TRoot, TEntity> AsFunction(IGraphQLFunction<TRoot> graphQLFunction)
        {
            var functionType = graphQLFunction.GetType();
            var arguments = functionType.GetProperties();

            var argumentStatements = arguments.Select(x => new GraphQLValueStatement(x.Name, _graphQLValueFactory.Construct(x.GetValue(graphQLFunction))));
            var argumentStatementObject = new GraphQLValueStatement(Constant.GraphQLKeyords.Args, new GraphQLObjectValue(argumentStatements));

            _graphQLQuery.HeaderNode.Title = functionType.Name;
            _graphQLQuery.HeaderNode.Statements.Add(argumentStatementObject);

            return this;
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

        IGraphQLSingleQueryBuilder<TRoot> IGraphQLRootNodeBuilder<TRoot>.ByPrimaryKey<TPrimaryKey>(Expression<Func<TRoot, TPrimaryKey>> propertyExpression, TPrimaryKey value)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(propertyExpression);
            return ByPrimaryKey(expressionStatement.PropertyName, value);
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

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.Where(Expression<Func<TRoot, bool>> expressionPredicate)
        {
            return Where(expressionPredicate);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Where(Expression<Func<TEntity, bool>> expressionPredicate)
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

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Limit(int number, int offset)
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

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.Limit(int number, int offset)
        {
            return Limit(number, offset);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> ResolveOrderByStatement<TNode, TKey>(Expression<Func<TNode, TKey>> keySelector, OrderByDirection orderByDirection)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(keySelector, orderByDirection);
            var expressionStatementObject = new GraphQLObjectValue(expressionStatement);

            var existingOrderByStatement = _graphQLSelectNode.HeaderNode.Statements.Find<GraphQLValueStatement>(Constant.GraphQLKeyords.OrderBy);
            if (existingOrderByStatement is null)
            {
                var orderByStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.OrderBy, expressionStatementObject);
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> DistinctOn<TNode, TKey>(Expression<Func<TNode, TKey>> keySelector)
        {
            var distinctSelector = _graphQLExpressionConverter.Convert(keySelector);
            var distinctStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.DistinctOn, new GraphQLObjectValue(distinctSelector));
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot>)this).OrderBy(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot, TEntity>)this).OrderBy(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderByDescending(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>)this).OrderByDescending(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot>)this).OrderByDescending(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot, TEntity>)this).OrderByDescending(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot, TEntity>)this).OrderByNullsFirst(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot>)this).OrderByDescendingNullsLast(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot>)this).OrderByDescendingNullsLast(keySelector);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLStandardNodeBuilder<TRoot, TEntity>)this).OrderByDescendingNullsLast(keySelector);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ((IGraphQLSingleNodeBuilder<TRoot, TEntity>)this).OrderByDescendingNullsLast(keySelector);
        }

        private GraphQLAggregateBuilder<TRoot, TEntity, TAggregate> Aggregate<TAggregate>()
            where TAggregate : IGraphQLEntity
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

                if (aggregateNode.EntityType.Equals(_graphQLSelectNode.EntityType))
                    aggregateNode.HeaderNode.Statements = _graphQLSelectNode.HeaderNode.Statements;

                var aggregateBuilder = new GraphQLAggregateBuilder<TRoot, TEntity, TAggregate>(_graphQLQuery, aggregateNode, _graphQLExpressionConverter, _graphQLValueFactory)
                {
                    _nodeBuilders = _nodeBuilders
                };

                _aggregateBuilders.Add(key, aggregateBuilder);
                builder = aggregateBuilder;
            }

            return (GraphQLAggregateBuilder<TRoot, TEntity, TAggregate>)builder;
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.Aggregate()
        {
            return Aggregate<TRoot>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TAggregate> IGraphQLSingleOrderedNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TAggregate> IGraphQLStandardOrderedNodeBuilder<TRoot>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLStandardAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLSingleAggregateBuilder<TRoot, TEntity, TAggregate> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Aggregate<TAggregate>()
        {
            return Aggregate<TAggregate>();
        }

        IGraphQLRootAggregateBuilder<TRoot> IGraphQLSingleQueryBuilder<TRoot>.Aggregate()
        {
            return Aggregate<TRoot>();
        }

        private GraphQLQueryBuilder<TRoot, TNode> Node<TNode>()
            where TNode : IGraphQLEntity
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

            return (GraphQLQueryBuilder<TRoot, TNode>)builder;
        }

        IGraphQLSingleNodeBuilder<TRoot, TNode> IGraphQLSingleNodeBuilderBase<TRoot>.Node<TNode>()
        {
            return Node<TNode>();
        }

        IGraphQLStandardNodeBuilder<TRoot, TNode> IGraphQLStandardNodeBuilderBase<TRoot>.Node<TNode>()
        {
            return Node<TNode>();
        }

        IGraphQLStandardQuery<TRoot> IGraphQLStandardQueryBuilderBase<TRoot>.Build()
        {
            return (IGraphQLStandardQuery<TRoot>)_graphQLQuery;
        }

        IGraphQLSingleQuery<TRoot> IGraphQLSingleQueryBuilder<TRoot>.Build()
        {
            _graphQLQuery.IsSingle = true;
            return (IGraphQLSingleQuery<TRoot>)_graphQLQuery;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLStandardQueryBuilder<TRoot>.Single(Expression<Func<TRoot, bool>> expressionPredicate)
        {
            if (expressionPredicate is null)
                return this;

            Node<TRoot>().Where(expressionPredicate);

            return this;
        }

        private GraphQLSelectedMethodConstruct<TRoot, TResult> Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            var expressionStatement = _graphQLExpressionConverter.ConvertSelectExpression(selector); 
            expressionStatement.ApplySelectStatement(_graphQLQuery.SelectNode, _graphQLSelectNodeFactory);
            
            var selectorFunc = selector.Compile();
            var query = new GraphQLSelectedMethodConstruct<TRoot, TResult>(GraphQLMethod.Query, _graphQLQuery.HeaderNode, _graphQLQuery.SelectNode, selectorFunc);

            return query;
        }

        private GraphQLSelectedMethodConstruct<TRoot, TResult> SelectSingle<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            var query = Select(selector);
            query.IsSingle = true;

            return query;
        }

        IGraphQLSingleSelectedQuery<TRoot, TResult> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return SelectSingle(selector);
        }

        IGraphQLSingleSelectedQuery<TRoot, TResult> IGraphQLSingleQueryBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return SelectSingle(selector);
        }

        IGraphQLStandardSelectedQuery<TRoot, TResult> IGraphQLStandardNodeBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        IGraphQLStandardSelectedQuery<TRoot, TResult> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        IGraphQLStandardSelectedQuery<TRoot, TResult> IGraphQLStandardOrderedNodeBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        IGraphQLStandardSelectedQuery<TRoot, TResult> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> Include<TNode>(Expression<Func<TRoot, TNode>> expression)
        {
            var expressionStatement = _graphQLExpressionConverter.ConvertSelectExpression(expression);
            expressionStatement.ApplyIncludeStatement(_graphQLSelectNode, _graphQLSelectNodeFactory);
            return this;
        }

        IGraphQLSingleQueryBuilder<TRoot> IGraphQLSingleQueryBuilder<TRoot>.Include<TNode>(Expression<Func<TRoot, TNode>> node)
        {
            return Include(node);
        }

        IGraphQLSingleQueryBuilder<TRoot> IGraphQLSingleQueryBuilder<TRoot>.Include<TNode>(Expression<Func<TRoot, IEnumerable<TNode>>> node)
        {
            return Include(node);
        }

        IGraphQLStandardQueryBuilder<TRoot> IGraphQLStandardQueryBuilder<TRoot>.Include<TNode>(Expression<Func<TRoot, TNode>> node)
        {
            return Include(node);
        }

        IGraphQLStandardQueryBuilder<TRoot> IGraphQLStandardQueryBuilder<TRoot>.Include<TNode>(Expression<Func<TRoot, IEnumerable<TNode>>> node)
        {
            return Include(node);
        }
    }
}