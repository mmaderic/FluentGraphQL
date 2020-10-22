using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    [Collection("Context collection")]
    public class OrderByTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public OrderByTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task OrderByStringPropertyNameTests()
        {
            var queryA = _graphQLClient.QueryBuilder<Product>()
                .OrderBy("Name")
                .Limit(1)
                .Select(x => x.Name);

            var resultA = await _graphQLClient.ExecuteAsync(queryA);
            var productA = resultA.Single();

            Assert.Equal("Access Hybrid EX 625 Allroad 29 titan´n´berry", productA);

            var queryB = _graphQLClient.QueryBuilder<Product>()
                .OrderBy("Brand.Name")
                .ThenBy("Price")
                .Limit(1)
                .Select(x => x.Name);

            var resultB = await _graphQLClient.ExecuteAsync(queryB);
            var productB = resultB.Single();

            Assert.Equal("Liberty IGR+ LS", productB);
        }
    }
}
