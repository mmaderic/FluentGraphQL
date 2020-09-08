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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentGraphQL.Client.Converters
{
    internal class GraphQLActionResponseJsonConverter<TResult> : JsonConverter<IGraphQLActionResponse<TResult>>, IGraphQLJsonConverter
    {
        public override IGraphQLActionResponse<TResult> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<GraphQLActionResponse<TResult>>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, IGraphQLActionResponse<TResult> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
