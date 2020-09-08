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

using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Responses;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentGraphQL.Client.Converters
{
    internal class GraphQLMutationReturningResponseJsonConverter<TReturn> : JsonConverter<IGraphQLMutationReturningResponse<TReturn>>, IGraphQLJsonConverter
    {
        public override IGraphQLMutationReturningResponse<TReturn> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var response = new GraphQLMutationReturningResponse<TReturn>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return response;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                reader.Read();

                if (reader.TokenType == JsonTokenType.Number)
                    response.AffectedRows = JsonSerializer.Deserialize<int>(ref reader);
                else if (reader.TokenType == JsonTokenType.StartArray)
                    response.Returning = JsonSerializer.Deserialize<List<TReturn>>(ref reader, options);
                else
                    throw new JsonException();
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IGraphQLMutationReturningResponse<TReturn> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
