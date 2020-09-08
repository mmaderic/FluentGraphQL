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
using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Extensions;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FluentGraphQL.Client.Services
{
    internal class GraphQLSerializerOptionsProvider : IGraphQLSerializerOptionsProvider
    {
        private readonly IGraphQLStringFactoryOptions _graphQLStringFactoryOptions;
        private readonly IGraphQLCustomResponseJsonConverterProvider _graphQLCustomResponseJsonConverterProvider;
        private readonly IGraphQLAggregateJsonConverterProvider _graphQLAggregateJsonConverterProvider;

        public GraphQLSerializerOptionsProvider(
            IGraphQLStringFactoryOptions graphQLStringFactoryOptions, IGraphQLCustomResponseJsonConverterProvider graphQLCustomResponseJsonConverterProvider,
            IGraphQLAggregateJsonConverterProvider graphQLAggregateJsonConverterProvider)
        {
            _graphQLStringFactoryOptions = graphQLStringFactoryOptions;
            _graphQLCustomResponseJsonConverterProvider = graphQLCustomResponseJsonConverterProvider;
            _graphQLAggregateJsonConverterProvider = graphQLAggregateJsonConverterProvider;
        }

        public JsonSerializerOptions Provide(IGraphQLMethodConstruct graphQLMethodConstruct = null)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = ResolveNamingPolicy() };
            var converters = _graphQLCustomResponseJsonConverterProvider.Provide();
            foreach (var converter in converters)
                options.Converters.Add((JsonConverter)converter);

            if (graphQLMethodConstruct is null)
                return options;

            if (ShouldIncludeAggregateContainerConverters(graphQLMethodConstruct))
            {
                converters = _graphQLAggregateJsonConverterProvider.Provide();
                foreach (var converter in converters)
                    options.Converters.Add((JsonConverter)converter);
            }

            return options;
        }

        private JsonNamingPolicy ResolveNamingPolicy()
        {
            switch (_graphQLStringFactoryOptions.NamingStrategy)
            {
                case NamingStrategy.SnakeCase:
                    return JsonNamingPolicyExtensions.SnakeCase();
                default:
                    throw new NotImplementedException(_graphQLStringFactoryOptions.NamingStrategy.ToString());
            }
        }

        private bool ShouldIncludeAggregateContainerConverters(IGraphQLMethodConstruct graphQLMethodConstruct)
        {
            if (graphQLMethodConstruct is IGraphQLQuery graphQLQuery)
            {
                var responseType = graphQLQuery.GetType().GenericTypeArguments.First();
                return
                    typeof(IGraphQLAggregateContainerNode).IsAssignableFrom(responseType) ||
                    graphQLQuery.HasAggregateContainer();
            }

            return false;
        }
    }
}
