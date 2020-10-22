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
        protected readonly IGraphQLNodeConstruct _graphQLNodeConstruct;
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
            _graphQLNodeConstruct = new GraphQLMethodConstruct<TEntity>(GraphQLMethod.Query, selectNode.HeaderNode, selectNode);
            _graphQLSelectNode = selectNode;
        }

        public GraphQLQueryBuilder(
            IGraphQLNodeConstruct graphQLNodeConstruct, IGraphQLSelectNode graphQLSelectNode,
            IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLValueFactory graphQLValuefactory)
            : this(graphQLExpressionConverter, graphQLValuefactory)
        {
            _graphQLNodeConstruct = graphQLNodeConstruct;
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

            _graphQLNodeConstruct.HeaderNode.Title = functionType.Name;
            _graphQLNodeConstruct.HeaderNode.Statements.Add(argumentStatementObject);

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

        private GraphQLQueryBuilder<TRoot, TEntity> Where(Expression expression)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(expression);
            return Where(expressionStatement);
        }      

        private GraphQLQueryBuilder<TRoot, TEntity> Where(IGraphQLValueStatement expressionStatement)
        {
            var expressionStatementObject = new GraphQLObjectValue(expressionStatement);
            var whereStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Where, expressionStatementObject);

            _graphQLSelectNode.HeaderNode.Statements.Add(whereStatement);

            return this;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLStandardQueryBuilder<TRoot>.Single()
        {
            return this;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLStandardQueryBuilder<TRoot>.Single(Expression expression)
        {
            Node<TRoot>().Where(expression);
            return this;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLStandardQueryBuilder<TRoot>.Single(Expression<Func<TRoot, bool>> expressionPredicate)
        {
            Node<TRoot>().Where(expressionPredicate);
            return this;
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.Where(Expression expression)
        {
            return Where(expression);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.Where(Expression expression)
        {
            return Where(expression);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Where(Expression expression)
        {
            return Where(expression);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Where(Expression expression)
        {
            return Where(expression);
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

        private GraphQLQueryBuilder<TRoot, TEntity> Limit(int value)
        {
            var limitStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Limit, _graphQLValueFactory.Construct(value));
            _graphQLSelectNode.HeaderNode.Statements.Add(limitStatement);

            return this;
        }

        private GraphQLQueryBuilder<TRoot, TEntity> Offset(int value)
        {
            var offsetStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Offset, _graphQLValueFactory.Construct(value));
            _graphQLSelectNode.HeaderNode.Statements.Add(offsetStatement);

            return this;
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.Limit(int value)
        {
            return Limit(value);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.Offset(int value)
        {
            return Offset(value);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.Offset(int value)
        {
            return Offset(value);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> DistinctOn(IGraphQLValueStatement distinctSelector)
        {
            GraphQLCollectionValue collectionValue;
            if (!(distinctSelector.PropertyName is null))
                collectionValue = new GraphQLCollectionValue(new[] { new GraphQLPropertyStatement(distinctSelector.PropertyName) });
            else
            {
                var propertyStatements = ((GraphQLObjectValue)distinctSelector.Value).PropertyValues.Select(x => new GraphQLPropertyStatement(x.PropertyName));
                collectionValue = new GraphQLCollectionValue(propertyStatements);
            }

            var distinctStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.DistinctOn, collectionValue);
            _graphQLSelectNode.HeaderNode.Statements.Add(distinctStatement);

            return this;
        }

        private GraphQLQueryBuilder<TRoot, TEntity> DistinctOn(Expression keySelector)
        {
            var distinctSelector = _graphQLExpressionConverter.Convert(keySelector);
            return DistinctOn(distinctSelector);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> DistinctOn(string[] propertyNames)
        {
            var propertyValues = propertyNames.Select(x => new GraphQLValueStatement(x, null));
            var valueStatement = new GraphQLValueStatement(null, new GraphQLObjectValue(propertyValues));
          
            return DistinctOn(valueStatement);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.DistinctOn(params string[] propertyNames)
        {
            return DistinctOn(propertyNames);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLSingleNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLSingleNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLStandardNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.DistinctOn<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        IGraphQLStandardNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.DistinctOn<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return DistinctOn(keySelector);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> ResolveOrderByStatement(IGraphQLValueStatement expressionStatement)
        {
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

        private GraphQLQueryBuilder<TRoot, TEntity> ResolveOrderByStatement(string propertyName, OrderByDirection orderByDirection)
        {
            var splits = propertyName.Split('.');
            var orderByStatement = _graphQLValueFactory.Construct(orderByDirection);
            var rootStatement = new GraphQLValueStatement(splits[0], null);
            IGraphQLValueStatement nestedStatement = rootStatement;

            for (int i = 1; i < splits.Length; i++)
            {
                nestedStatement.Value = new GraphQLObjectValue(new GraphQLValueStatement(splits[i], null));
                nestedStatement = ((GraphQLObjectValue)nestedStatement.Value).PropertyValues.First();
            }

            nestedStatement.Value = orderByStatement;
            return ResolveOrderByStatement(rootStatement);
        }

        private GraphQLQueryBuilder<TRoot, TEntity> ResolveOrderByStatement(Expression expression, OrderByDirection orderByDirection)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(expression, orderByDirection);
            return ResolveOrderByStatement(expressionStatement);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleNodeBuilder<TRoot>.OrderByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardNodeBuilder<TRoot>.OrderByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardNodeBuilder<TRoot, TEntity>.OrderByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
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

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenBy(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescendingNullsLast(string propertyName)
        {
            return ResolveOrderByStatement(propertyName, OrderByDirection.DescNullsLast);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenBy<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Asc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByDescending<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.Desc);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByNullsFirst<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByNullsFirst<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.AscNullsFirst);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot> IGraphQLSingleOrderedNodeBuilder<TRoot>.ThenByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot> IGraphQLStandardOrderedNodeBuilder<TRoot>.ThenByDescendingNullsLast<TKey>(Expression<Func<TRoot, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.ThenByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
        }

        IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.ThenByDescendingNullsLast<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return ResolveOrderByStatement(keySelector, OrderByDirection.DescNullsLast);
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

                var aggregateBuilder = new GraphQLAggregateBuilder<TRoot, TEntity, TAggregate>(_graphQLNodeConstruct, aggregateNode, _graphQLExpressionConverter, _graphQLValueFactory)
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
                var node = _graphQLSelectNode.FindNode<TNode>();
                if (node is null)
                    throw new InvalidOperationException("Selected node is not part of the query.");

                var queryBuilder = new GraphQLQueryBuilder<TRoot, TNode>(_graphQLNodeConstruct, node, _graphQLExpressionConverter, _graphQLValueFactory)
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

        IGraphQLArrayQuery<TRoot> IGraphQLStandardQueryBuilderBase<TRoot>.Build()
        {
            return (IGraphQLArrayQuery<TRoot>)_graphQLNodeConstruct;
        }

        IGraphQLObjectQuery<TRoot> IGraphQLSingleQueryBuilder<TRoot>.Build()
        {
            _graphQLNodeConstruct.IsSingle = true;
            return (IGraphQLObjectQuery<TRoot>)_graphQLNodeConstruct;
        }

        private GraphQLSelectedMethodConstruct<TRoot, TResult> Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            var expressionStatement = _graphQLExpressionConverter.ConvertSelectExpression(selector); 
            expressionStatement.ApplySelectStatement(_graphQLNodeConstruct.SelectNode, _graphQLSelectNodeFactory);
            
            var selectorFunc = selector.Compile();
            var query = new GraphQLSelectedMethodConstruct<TRoot, TResult>(GraphQLMethod.Query, _graphQLNodeConstruct.HeaderNode, _graphQLNodeConstruct.SelectNode, selectorFunc);

            return query;
        }

        private GraphQLSelectedMethodConstruct<TRoot, TResult> SelectSingle<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            var query = Select(selector);
            query.IsSingle = true;

            return query;
        }

        IGraphQLSelectedObjectQuery<TRoot, TResult> IGraphQLSingleOrderedNodeBuilder<TRoot, TEntity>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return SelectSingle(selector);
        }

        IGraphQLSelectedObjectQuery<TRoot, TResult> IGraphQLSingleQueryBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return SelectSingle(selector);
        }

        IGraphQLSelectedArrayQuery<TRoot, TResult> IGraphQLStandardNodeBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        IGraphQLSelectedArrayQuery<TRoot, TResult> IGraphQLStandardNodeBuilder<TRoot, TEntity>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        IGraphQLSelectedArrayQuery<TRoot, TResult> IGraphQLStandardOrderedNodeBuilder<TRoot>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
        {
            return Select(selector);
        }

        IGraphQLSelectedArrayQuery<TRoot, TResult> IGraphQLStandardOrderedNodeBuilder<TRoot, TEntity>.Select<TResult>(Expression<Func<TRoot, TResult>> selector)
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