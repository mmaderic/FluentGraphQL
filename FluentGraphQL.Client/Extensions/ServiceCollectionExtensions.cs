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
using FluentGraphQL.Builder.Converters;
using FluentGraphQL.Builder.Factories;
using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Options;
using FluentGraphQL.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace FluentGraphQL.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQLClient(this IServiceCollection serviceCollection, Func<IServiceProvider, GraphQLOptions> optionsFactory)
        {
            serviceCollection.TryAddSingleton<IGraphQLValueFactory, GraphQLValueFactory>();
            serviceCollection.TryAddSingleton<IGraphQLStringFactory, GraphQLStringFactory>();
            serviceCollection.TryAddSingleton<IGraphQLValueConverter, GraphQLValueConverter>();
            serviceCollection.TryAddSingleton<IGraphQLSelectNodeFactory, GraphQLSelectNodeFactory>();
            serviceCollection.TryAddSingleton<IGraphQLBuilderFactory, GraphQLBuilderFactory>();
            serviceCollection.TryAddSingleton<IGraphQLExpressionConverter, GraphQLExpressionConverter>();
            serviceCollection.TryAddSingleton<IGraphQLAggregateJsonConverterProvider, GraphQLAggregateJsonConverterProvider>();
            serviceCollection.TryAddSingleton<IGraphQLCustomResponseJsonConverterProvider, GraphQLCustomResponseJsonConverterProvider>();
            serviceCollection.TryAddSingleton<IGraphQLSerializerOptionsProvider, GraphQLSerializerOptionsProvider>();
            serviceCollection.TryAddSingleton<IGraphQLWebSocketProtocolService, GraphQLWebSocketProtocolService>();

            serviceCollection.AddScoped<IGraphQLClient, GraphQLClient>();

            serviceCollection.AddTransient<IGraphQLClientOptions>(x => optionsFactory.Invoke(x));
            serviceCollection.AddTransient<IGraphQLStringFactoryOptions>(x => optionsFactory.Invoke(x));
            serviceCollection.AddTransient<IGraphQLSubscriptionOptions>(x => optionsFactory.Invoke(x));

            return serviceCollection;
        }
    }
}
