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
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentGraphQL.Client.Converters
{
    internal class GraphQLMutationReturningResponseJsonConverterFactory : JsonConverterFactory, IGraphQLJsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
                return false;

            if (!typeToConvert.GetGenericTypeDefinition().Equals(typeof(IGraphQLMutationReturningResponse<>)))
                return false;

            return true;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var returnType = typeToConvert.GetGenericArguments().First();
            var type = typeof(GraphQLMutationReturningResponseJsonConverter<>).MakeGenericType(returnType);

            return (JsonConverter)Activator.CreateInstance(type);
        }
    }
}
