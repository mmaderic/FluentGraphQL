using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests
{
    [Collection("Context collection")]
    public class ExtensionMethodsTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public ExtensionMethodsTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task SelectManyTest()
        {
            var query = _graphQLClient.QueryBuilder<Brand>()
                .ByPrimaryKey(x => x.Id, Context.Brands.Cube.Id)
                .Select(x => x.Products.SelectMany(y => y.Stocks));

            var result = await _graphQLClient.ExecuteAsync(query);
            var type = result.GetType().GenericTypeArguments.Last();

            Assert.Equal(typeof(Stock), type);
            Assert.Equal(36, result.Count());
        }
    }
}
