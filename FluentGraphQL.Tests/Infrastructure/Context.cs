using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluentGraphQL.Tests.Infrastructure
{
    public class Context : IDisposable
    {
        public void Dispose()
        {
            var client = Configuration.ServiceProvider.GetRequiredService<IGraphQLClient>();
        }
    }
}
