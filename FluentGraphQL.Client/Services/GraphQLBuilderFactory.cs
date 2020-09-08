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
using FluentGraphQL.Builder.Builders;
using FluentGraphQL.Client.Abstractions;

namespace FluentGraphQL.Client.Services
{
    internal class GraphQLBuilderFactory : IGraphQLBuilderFactory
    {
        private readonly IGraphQLExpressionConverter _graphQLExpressionConverter;
        private readonly IGraphQLValueFactory _graphQLValueFactory;
        private readonly IGraphQLSelectNodeFactory _graphQLSelectNodeFactory;

        public GraphQLBuilderFactory(
            IGraphQLExpressionConverter graphQLExpressionConverter,
            IGraphQLValueFactory graphQLValueFactory, IGraphQLSelectNodeFactory graphQLSelectNodeFactory)
        {
            _graphQLExpressionConverter = graphQLExpressionConverter;
            _graphQLValueFactory = graphQLValueFactory;
            _graphQLSelectNodeFactory = graphQLSelectNodeFactory;
        }

        public IGraphQLRootNodeBuilder<TEntity> QueryBuilder<TEntity>()
            where TEntity : IGraphQLEntity
        {
            return new GraphQLQueryBuilder<TEntity, TEntity>(_graphQLSelectNodeFactory, _graphQLExpressionConverter, _graphQLValueFactory);
        }

        public IGraphQLMutationBuilder<TEntity> MutationBuilder<TEntity>() 
            where TEntity : IGraphQLEntity
        {
            return new GraphQLMutationBuilder<TEntity>(_graphQLSelectNodeFactory, _graphQLValueFactory, _graphQLExpressionConverter);
        }

        public IGraphQLActionBuilder ActionBuilder()
        {
            return new GraphQLActionBuilder(_graphQLSelectNodeFactory, _graphQLValueFactory);
        }
    }
}
