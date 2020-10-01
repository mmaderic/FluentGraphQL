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
using FluentGraphQL.Builder.Nodes;
using FluentGraphQL.Client.Abstractions;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentGraphQL.Client.Converters
{
    internal class GraphQLAggregateClauseJsonConverter : JsonConverter<IGraphQLAggregateClauseNode>, IGraphQLJsonConverter
    {
        private readonly IGraphQLStringFactory _graphQLStringFactory;

        public GraphQLAggregateClauseJsonConverter(IGraphQLStringFactory graphQLStringFactory)
        {
            _graphQLStringFactory = graphQLStringFactory;
        }

        public override IGraphQLAggregateClauseNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var aggregateClause = new GraphQLAggregateClauseNode();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return aggregateClause;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                var propertyName = _graphQLStringFactory.Desconstruct(reader.GetString());
                reader.Read();

                object propertyValue;
                if (reader.TokenType == JsonTokenType.Number)                
                    propertyValue = JsonSerializer.Deserialize<double>(ref reader, options);                
                else if (reader.TokenType == JsonTokenType.String)
                    propertyValue = JsonSerializer.Deserialize<string>(ref reader, options);
                else if (reader.TokenType == JsonTokenType.Null)
                    propertyValue = null;
                else
                    throw new NotImplementedException(reader.TokenType.ToString());

                aggregateClause.PropertyValues.Add(propertyName, propertyValue);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IGraphQLAggregateClauseNode value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
