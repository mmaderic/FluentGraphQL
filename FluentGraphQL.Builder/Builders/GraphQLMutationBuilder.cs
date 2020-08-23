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
    public class GraphQLMutationBuilder<TEntity> : IGraphQLInsertSingleMutationBuilder<TEntity>, IGraphQLInsertMultipleMutationBuilder<TEntity>
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
            void RemoveNullValues(IGraphQLValue graphQLValueInstance)
            {
                var objectValue = (GraphQLObjectValue)graphQLValueInstance;
                objectValue.PropertyValues = objectValue.PropertyValues.Where(x => !x.Value.IsNull());
            }

            var isSingle = !(value is IEnumerable);
            var graphQLValue = _graphQLValueFactory.Construct(value);

            if (isSingle)
            {
                _graphQLSelectNode = _graphQLSelectNodeFactory.Construct(typeof(TEntity));
                _graphQLSelectNode.HeaderNode.Suffix = Constant.GraphQLKeyords.One;
                RemoveNullValues(graphQLValue);
            }
            else
            {
                _graphQLSelectNode = _graphQLSelectNodeFactory.Construct(typeof(GraphQLMultipleInsertSelect<TEntity>));
                _graphQLSelectNode.HeaderNode.Title = typeof(TEntity).Name;

                var collectionValue = (GraphQLCollectionValue)graphQLValue;
                Parallel.ForEach(collectionValue.CollectionItems, (item) => { RemoveNullValues(item); });
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

        private GraphQLMutation BuildInsertMutation<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression = null)
        {
            if (!(returnExpression is null))
            {
                var expressionStatement = _graphQLExpressionConverter.ConvertSelectExpression(returnExpression);
                var selectNode = _graphQLSelectNode.ChildSelectNodes.FirstOrDefault(x => x.HeaderNode.Title.Equals(Constant.GraphQLKeyords.Returning));
                if (selectNode is null)
                    selectNode = _graphQLSelectNode;

                expressionStatement.ApplySelectStatement(selectNode);

                return new GraphQLSelectedMutation<TEntity, TReturn>(_graphQLSelectNode.HeaderNode, _graphQLSelectNode, returnExpression.Compile());
            }

            return new GraphQLMutation<TEntity>(_graphQLSelectNode.HeaderNode, _graphQLSelectNode);            
        }

        IGraphQLSelectedInsertSingleMutation<TEntity, TReturn> IGraphQLInsertSingleMutationBuilder<TEntity>.Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression)
        {
            return (IGraphQLSelectedInsertSingleMutation<TEntity, TReturn>)BuildInsertMutation(returnExpression);
        }

        IGraphQLSelectedInsertMultipleMutation<TEntity, TReturn> IGraphQLInsertMultipleMutationBuilder<TEntity>.Return<TReturn>(Expression<Func<TEntity, TReturn>> returnExpression)
        {
            return (IGraphQLSelectedInsertMultipleMutation<TEntity, TReturn>)BuildInsertMutation(returnExpression);
        }

        IGraphQLInsertSingleMutation<TEntity> IGraphQLInsertSingleMutationBuilder<TEntity>.Build()
        {
            return (IGraphQLInsertSingleMutation<TEntity>)BuildInsertMutation<TEntity>();
        }

        IGraphQLInsertMultipleMutation<TEntity> IGraphQLInsertMultipleMutationBuilder<TEntity>.Build()
        {
            return (IGraphQLInsertMultipleMutation<TEntity>)BuildInsertMutation<TEntity>();
        }
    }
}
