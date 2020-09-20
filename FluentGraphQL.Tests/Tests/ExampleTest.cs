using FluentGraphQL.Client.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    public class ExampleTest : IClassFixture<Setup>
    {
        private readonly IGraphQLClient _graphQLClient;

        public ExampleTest(Setup setup)
        {
            _graphQLClient = setup.ServiceProvider.GetRequiredService<IGraphQLClient>();
        }

        [Fact]
        public void ExampleTestOne()
        {
            Assert.True(true);
        }
    }
}
