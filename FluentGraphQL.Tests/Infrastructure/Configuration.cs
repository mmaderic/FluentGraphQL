using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Client.Extensions;
using FluentGraphQL.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace FluentGraphQL.Tests.Infrastructure
{
    public static class Configuration
    {
        public static IServiceProvider ServiceProvider { get; } 

        static Configuration()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddGraphQLClient(x => new GraphQLOptions
            {
                UseAdminHeader = true,
                AdminHeaderName = "x-hasura-admin-secret",
                AdminHeaderSecret = "admin",
                WebSocketEndpoint = "ws://localhost:8080/v1/graphql",
                NamingStrategy = NamingStrategy.SnakeCase,
                HttpClientProvider = () => new HttpClient { BaseAddress = new Uri("http://hasura:8080/v1/graphql") }
            });

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
