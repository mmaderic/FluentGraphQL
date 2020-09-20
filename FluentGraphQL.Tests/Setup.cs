using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Client.Extensions;
using FluentGraphQL.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace FluentGraphQL.Tests
{
    public class Setup
    {
        public IServiceProvider ServiceProvider { get; } 

        public Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddGraphQLClient(x => new GraphQLOptions
            {
                UseAdminHeader = true,
                AdminHeaderName = "x-hasura-admin-secret",
                AdminHeaderSecret = "admin",
                WebSocketEndpoint = "ws://localhost:8080/v1/graphql",
                NamingStrategy = NamingStrategy.SnakeCase,
                HttpClientProvider = () => new HttpClient { BaseAddress = new Uri("http://localhost:8080/v1/graphql") }
            });

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
