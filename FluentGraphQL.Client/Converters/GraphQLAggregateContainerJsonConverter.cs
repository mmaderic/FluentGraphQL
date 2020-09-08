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
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Nodes;
using FluentGraphQL.Client.Abstractions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentGraphQL.Client.Converters
{
    internal class GraphQLAggregateContainerJsonConverter<TEntity> : JsonConverter<IGraphQLAggregateContainerNode<TEntity>>, IGraphQLJsonConverter
    {
        private readonly IGraphQLExpressionConverter _graphQLExpressionConverter;
        private readonly IGraphQLStringFactory _graphQLStringFactory;
        private GraphQLAggregateJsonConverter _aggregateConverter;

        public GraphQLAggregateContainerJsonConverter(IGraphQLExpressionConverter graphQLExpressionConverter, IGraphQLStringFactory graphQLStringFactory)
        {
            _graphQLExpressionConverter = graphQLExpressionConverter;
            _graphQLStringFactory = graphQLStringFactory;
        }

        public override IGraphQLAggregateContainerNode<TEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var aggregateType = typeof(IGraphQLAggregateNode);
            var aggregateContainer = new GraphQLAggregateContainerNode<TEntity>(_graphQLExpressionConverter);

            _aggregateConverter = (GraphQLAggregateJsonConverter)options.GetConverter(aggregateType);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)                
                    return aggregateContainer;                

                if (reader.TokenType != JsonTokenType.PropertyName)                
                    throw new JsonException();                

                var propertyName = reader.GetString();
                if (propertyName.Equals(_graphQLStringFactory.Construct(Constant.GraphQLKeyords.Aggregate)))
                {
                    reader.Read();
                    aggregateContainer.Aggregate = _aggregateConverter.Read(ref reader, aggregateType, options);
                }
                else if (propertyName.Equals(_graphQLStringFactory.Construct(Constant.GraphQLKeyords.Nodes)))
                    aggregateContainer.Nodes = JsonSerializer.Deserialize<ICollection<TEntity>>(ref reader, options);                
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IGraphQLAggregateContainerNode<TEntity> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
