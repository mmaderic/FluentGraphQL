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
using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Converters;
using System.Collections.Generic;

namespace FluentGraphQL.Client.Services
{
    internal class GraphQLAggregateJsonConverterProvider : IGraphQLAggregateJsonConverterProvider
    {
        private readonly GraphQLAggregateJsonConverter _graphQLAggregateJsonConverter;
        private readonly GraphQLAggregateClauseJsonConverter _graphQLAggregateClauseJsonConverter;
        private readonly GraphQLAggregateContainerJsonConverterFactory _graphQLAggregateContainerJsonConverterFactory; 

        public GraphQLAggregateJsonConverterProvider(IGraphQLStringFactory graphQLStringFactory, IGraphQLExpressionConverter graphQLExpressionConverter)
        {
            _graphQLAggregateJsonConverter = new GraphQLAggregateJsonConverter();
            _graphQLAggregateClauseJsonConverter = new GraphQLAggregateClauseJsonConverter(graphQLStringFactory);
            _graphQLAggregateContainerJsonConverterFactory = new GraphQLAggregateContainerJsonConverterFactory(graphQLExpressionConverter, graphQLStringFactory);
        }

        public ICollection<IGraphQLJsonConverter> Provide()
        {
            return new IGraphQLJsonConverter[]
            {
                _graphQLAggregateJsonConverter,
                _graphQLAggregateClauseJsonConverter,
                _graphQLAggregateContainerJsonConverterFactory
            };
        }
    }
}
