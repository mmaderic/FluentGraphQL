using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    [Collection("Context collection")]
    public class DistinctOnTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public DistinctOnTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task ExpressionDistinctTests()
        {
            var queryA = _graphQLClient.QueryBuilder<Staff>()
                .Where(x => x.ManagerId != null)
                .DistinctOn(x => x.ManagerId)
                .Select(x => x.Manager!.FirstName);

            var resultA = await _graphQLClient.ExecuteAsync(queryA);

            Assert.Equal(2, resultA.Count);

            var queryB = _graphQLClient.QueryBuilder<Store>()
                .DistinctOn(x => x.City!)
                .Build();

            var resultB = await _graphQLClient.ExecuteAsync(queryB);

            Assert.Equal(2, resultB.Count);

            var queryC = _graphQLClient.QueryBuilder<Store>()
                .DistinctOn(x => new object[] { x.City!, x.ZipCode! })
                .Build();

            var resultC = await _graphQLClient.ExecuteAsync(queryC);

            Assert.Equal(3, resultC.Count);
        }

        [Fact]
        public async Task StringDistinctTests()
        {
            var queryA = _graphQLClient.QueryBuilder<Staff>()
                .Where(x => x.ManagerId != null)
                .DistinctOn("ManagerId")
                .Select(x => x.Manager!.FirstName);

            var resultA = await _graphQLClient.ExecuteAsync(queryA);

            Assert.Equal(2, resultA.Count);

            var queryB = _graphQLClient.QueryBuilder<Store>()
                .DistinctOn("City")
                .Build();

            var resultB = await _graphQLClient.ExecuteAsync(queryB);

            Assert.Equal(2, resultB.Count);

            var queryC = _graphQLClient.QueryBuilder<Store>()
                .DistinctOn(new [] { "City", "ZipCode" })
                .Build();

            var resultC = await _graphQLClient.ExecuteAsync(queryC);

            Assert.Equal(3, resultC.Count);
        }
    }
}
