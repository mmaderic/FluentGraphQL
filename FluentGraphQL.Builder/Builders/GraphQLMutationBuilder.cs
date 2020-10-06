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
using FluentGraphQL.Builder.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentGraphQL.Builder.Builders
{
    public class GraphQLMutationBuilder<TEntity> : IGraphQLMutationBuilder<TEntity>, 
        IGraphQLInsertSingleMutationBuilder<TEntity>, IGraphQLInsertMultipleMutationBuilder<TEntity>,
        IGraphQLUpdateSingleMutationBuilder<TEntity>, IGraphQLUpdateMultipleMutationBuilder<TEntity>
        where TEntity : IGraphQLEntity
    {
        private readonly IGraphQLSelectNodeFactory _graphQLSelectNodeFactory;
        private readonly IGraphQLValueFactory _graphQLValueFactory;
        private readonly IGraphQLExpressionConverter _graphQLExpressionConverter;

        private IGraphQLSelectNode _graphQLSelectNode;

        public GraphQLMutationBuilder(IGraphQLSelectNodeFactory graphQLSelectNodeFactory, IGraphQLValueFactory graphQLValueFactory, IGraphQLExpressionConverter graphQLExpressionConverter)
        {
            _graphQLSelectNodeFactory = graphQLSelectNodeFactory;
            _graphQLValueFactory = graphQLValueFactory;
            _graphQLExpressionConverter = graphQLExpressionConverter;
        }

        private GraphQLMutationBuilder<TEntity> Insert(object value)
        {     
            var isSingle = !(value is IEnumerable);
            var graphQLValue = _graphQLValueFactory.Construct(value);            

            if (isSingle)
            {
                _graphQLSelectNode = _graphQLSelectNodeFactory.Construct(typeof(TEntity));
                _graphQLSelectNode.HeaderNode.Suffix = Constant.GraphQLKeyords.One;

                RemoveNullValues(graphQLValue);
                ModifyNestedObjectInsertStatements((IGraphQLObjectValue)graphQLValue);
            }
            else
            {
                _graphQLSelectNode = _graphQLSelectNodeFactory.Construct(typeof(GraphQLMutationReturningSelectNode<TEntity>));
                _graphQLSelectNode.HeaderNode.Title = typeof(TEntity).Name;

                var collectionValue = (GraphQLCollectionValue)graphQLValue;
                Parallel.ForEach(collectionValue.CollectionItems, (item) => { RemoveNullValues(item); });
                Parallel.ForEach(collectionValue.CollectionItems, (item) => { ModifyNestedObjectInsertStatements((IGraphQLObjectValue)item); });
            }

            _graphQLSelectNode.HeaderNode.Prefix = Constant.GraphQLKeyords.Insert;

            var keyword = isSingle
                ? Constant.GraphQLKeyords.Object
                : Constant.GraphQLKeyords.Objects;

            var graphQLValueStatement = new GraphQLValueStatement(keyword, graphQLValue);
            _graphQLSelectNode.HeaderNode.Statements.Add(graphQLValueStatement);

            return this;
        }

        IGraphQLInsertSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.Insert(TEntity entity)
        {
            return Insert(entity);
        }

        IGraphQLInsertMultipleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.Insert(IEnumerable<TEntity> entities)
        {
            return Insert(entities);
        }

        private GraphQLMethodConstruct BuildMutation<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression = null)
        {
            if (!(returnExpression is null))
            {
                var expressionStatement = _graphQLExpressionConverter.ConvertSelectExpression(returnExpression);
                var selectNode = _graphQLSelectNode.ChildSelectNodes.FirstOrDefault(x => x.HeaderNode.Title.Equals(Constant.GraphQLKeyords.Returning));
                if (selectNode is null)
                    selectNode = _graphQLSelectNode;

                expressionStatement.ApplySelectStatement(selectNode, _graphQLSelectNodeFactory);

                return new GraphQLSelectedMethodConstruct<TEntity, TReturn>(GraphQLMethod.Mutation, _graphQLSelectNode.HeaderNode, _graphQLSelectNode, returnExpression.Compile());
            }

            return new GraphQLMethodConstruct<TEntity>(GraphQLMethod.Mutation, _graphQLSelectNode.HeaderNode, _graphQLSelectNode);            
        }

        IGraphQLSelectedReturnSingleMutation<TEntity, TReturn> IGraphQLReturnSingleMutationBuilder<TEntity>.Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression)
        {
            var mutation = BuildMutation(returnExpression);
            mutation.IsSingle = true;

            return (IGraphQLSelectedReturnSingleMutation<TEntity, TReturn>)mutation;
        }

        IGraphQLSelectedReturnMultipleMutation<TEntity, TReturn> IGraphQLReturnMultipleMutationBuilder<TEntity>.Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression)
        {
            return (IGraphQLSelectedReturnMultipleMutation<TEntity, TReturn>)BuildMutation(returnExpression);
        }

        IGraphQLReturnSingleMutation<TEntity> IGraphQLReturnSingleMutationBuilder<TEntity>.Build()
        {
            var mutation = BuildMutation<TEntity>(); 
            mutation.IsSingle = true;

            return (IGraphQLReturnSingleMutation<TEntity>)mutation;
        }

        IGraphQLReturnMultipleMutation<TEntity> IGraphQLReturnMultipleMutationBuilder<TEntity>.Build()
        {
            return (IGraphQLReturnMultipleMutation<TEntity>)BuildMutation<TEntity>();
        }

        private void RemoveNullValues(IGraphQLValue graphQLValue)
        {
            var objectValue = (GraphQLObjectValue)graphQLValue;
            objectValue.PropertyValues = objectValue.PropertyValues.Where(x => !x.Value.IsNull()).ToArray();
            foreach (var property in objectValue.PropertyValues)
            {
                if (property.Value is IGraphQLObjectValue)
                    RemoveNullValues(property.Value);
                else if (property.Value is IGraphQLCollectionValue collection)
                {
                    foreach (var item in collection.CollectionItems)
                        RemoveNullValues(item);
                }
            }
        }

        private void ModifyNestedObjectInsertStatements(IGraphQLObjectValue graphQLObjectValue)
        {
            Parallel.For(0, graphQLObjectValue.PropertyValues.Count(), (i) =>
            {
                var statement = graphQLObjectValue.PropertyValues.ElementAt(i);
                if (statement.Value is IGraphQLObjectValue || statement.Value is IGraphQLCollectionValue)
                {
                    statement.Value = new GraphQLObjectValue(new GraphQLValueStatement(Constant.GraphQLKeyords.Data, statement.Value));
                    graphQLObjectValue.PropertyValues = graphQLObjectValue.PropertyValues.ReplaceAt(i, statement);
                }
             });
        }

        private void InitializeSingleReturnBuilder(string keyword)
        {
            _graphQLSelectNode = _graphQLSelectNodeFactory.Construct(typeof(TEntity));
            _graphQLSelectNode.HeaderNode.Prefix = keyword;
            _graphQLSelectNode.HeaderNode.Suffix = Constant.GraphQLKeyords.ByPk;
        }

        private GraphQLMutationBuilder<TEntity> UpdateByPrimaryKey(string key, object value)
        {
            InitializeSingleReturnBuilder(Constant.GraphQLKeyords.Update);

            var graphQLValue = _graphQLValueFactory.Construct(value);
            var primaryKeyObjectValue = new GraphQLObjectValue(new GraphQLValueStatement(key, graphQLValue));
            var primaryKeyStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.PkColumns, primaryKeyObjectValue);

            _graphQLSelectNode.HeaderNode.Statements.Add(primaryKeyStatement);

            return this;
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.UpdateByPrimaryKey(string key, object value)
        {
            return UpdateByPrimaryKey(key, value);
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.UpdateById(object value)
        {
            return UpdateByPrimaryKey(Constant.GraphQLKeyords.Id, value);
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.UpdateByPrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(propertyExpression);
            return UpdateByPrimaryKey(expressionStatement.PropertyName, value);
        }

        private void InitializeMultipleReturnBuilder(string keyword)
        {
            _graphQLSelectNode = _graphQLSelectNodeFactory.Construct(typeof(GraphQLMutationReturningSelectNode<TEntity>));
            _graphQLSelectNode.HeaderNode.Title = typeof(TEntity).Name;
            _graphQLSelectNode.HeaderNode.Prefix = keyword;
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.UpdateAll()
        {
            InitializeMultipleReturnBuilder(Constant.GraphQLKeyords.Update);

            var whereStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Where, null);
            _graphQLSelectNode.HeaderNode.Statements.Add(whereStatement);

            return this;
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.UpdateWhere(Expression<Func<TEntity, bool>> expressionPredicate)
        {
            InitializeMultipleReturnBuilder(Constant.GraphQLKeyords.Update);

            var expressionStatement = _graphQLExpressionConverter.Convert(expressionPredicate);
            var expressionStatementObject = new GraphQLObjectValue(expressionStatement);
            var whereStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Where, expressionStatementObject);

            _graphQLSelectNode.HeaderNode.Statements.Add(whereStatement);

            return this;
        }

        private GraphQLMutationBuilder<TEntity> AddPropertyStatement<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value, string statementKey)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(propertyExpression);
            var graphQLValue = _graphQLValueFactory.Construct(value);
            var graphQLValueStatement = new GraphQLValueStatement(expressionStatement.PropertyName, graphQLValue);

            return AddPropertyStatement(graphQLValueStatement, statementKey);
        }

        private GraphQLMutationBuilder<TEntity> AddPropertyStatement(IGraphQLValueStatement graphQLValueStatement, string statementKey)
        {
            var existingStatement = _graphQLSelectNode.HeaderNode.Statements.Find<GraphQLValueStatement>(statementKey);
            if (existingStatement is null)
            {
                var graphQLObjectValue = new GraphQLObjectValue(graphQLValueStatement);
                _graphQLSelectNode.HeaderNode.Statements.Add(new GraphQLValueStatement(statementKey, graphQLObjectValue));
            }
            else
            {
                var setStatementValue = (GraphQLObjectValue)existingStatement.Value;
                setStatementValue.PropertyValues = setStatementValue.PropertyValues.Append(graphQLValueStatement);
            }

            return this;
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLUpdateSingleMutationBuilder<TEntity>.Set<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
        {
            return AddPropertyStatement(propertyExpression, value, Constant.GraphQLKeyords.Set);
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLUpdateMultipleMutationBuilder<TEntity>.Set<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value)
        {
            return AddPropertyStatement(propertyExpression, value, Constant.GraphQLKeyords.Set);
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLUpdateSingleMutationBuilder<TEntity>.Set(TEntity entity)
        {
            var graphQLObjectValue = (IGraphQLObjectValue)_graphQLValueFactory.Construct(entity);
            foreach (var property in graphQLObjectValue.PropertyValues)
            {
                if (!(property.Value is IGraphQLPropertyValue))
                    continue;

                AddPropertyStatement(property, Constant.GraphQLKeyords.Set);
            }

            return this;
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLUpdateSingleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, int>> propertyExpression, int incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLUpdateSingleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, long>> propertyExpression, long incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLUpdateSingleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, int?>> propertyExpression, int incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateSingleMutationBuilder<TEntity> IGraphQLUpdateSingleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, long?>> propertyExpression, long incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLUpdateMultipleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, int>> propertyExpression, int incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLUpdateMultipleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, long>> propertyExpression, long incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLUpdateMultipleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, int?>> propertyExpression, int incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }

        IGraphQLUpdateMultipleMutationBuilder<TEntity> IGraphQLUpdateMultipleMutationBuilder<TEntity>.Increment(Expression<Func<TEntity, long?>> propertyExpression, long incrementBy)
        {
            return AddPropertyStatement(propertyExpression, incrementBy, Constant.GraphQLKeyords.Increment);
        }       

        private GraphQLMutationBuilder<TEntity> DeleteByPrimaryKey(string key, object value)
        {
            InitializeSingleReturnBuilder(Constant.GraphQLKeyords.Delete);

            var graphQLValue = _graphQLValueFactory.Construct(value);
            var primaryKeyValue = new GraphQLValueStatement(key, graphQLValue);

            _graphQLSelectNode.HeaderNode.Statements.Add(primaryKeyValue);

            return this;
        }

        IGraphQLReturnSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.DeleteById(object idValue)
        {
            return DeleteByPrimaryKey(Constant.GraphQLKeyords.Id, idValue);
        }

        IGraphQLReturnSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.DeleteByPrimaryKey(string key, object primaryKeyValue)
        {
            return DeleteByPrimaryKey(key, primaryKeyValue);
        }

        IGraphQLReturnSingleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.DeleteByPrimaryKey<TProperty>(Expression<Func<TEntity, TProperty>> primaryKeySelector, TProperty primaryKeyValue)
        {
            var expressionStatement = _graphQLExpressionConverter.Convert(primaryKeySelector);
            return DeleteByPrimaryKey(expressionStatement.PropertyName, primaryKeyValue);
        }

        IGraphQLReturnMultipleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.DeleteWhere(Expression<Func<TEntity, bool>> expressionPredicate)
        {
            InitializeMultipleReturnBuilder(Constant.GraphQLKeyords.Delete);

            var expressionStatement = _graphQLExpressionConverter.Convert(expressionPredicate);
            var expressionStatementObject = new GraphQLObjectValue(expressionStatement);
            var whereStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Where, expressionStatementObject);

            _graphQLSelectNode.HeaderNode.Statements.Add(whereStatement);

            return this;
        }

        IGraphQLReturnMultipleMutationBuilder<TEntity> IGraphQLMutationBuilder<TEntity>.DeleteAll()
        {
            InitializeMultipleReturnBuilder(Constant.GraphQLKeyords.Delete);

            var whereStatement = new GraphQLValueStatement(Constant.GraphQLKeyords.Where, null);
            _graphQLSelectNode.HeaderNode.Statements.Add(whereStatement);

            return this;
        }
    }
}
