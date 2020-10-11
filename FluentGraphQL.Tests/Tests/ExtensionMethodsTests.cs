using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Extensions;
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

        [Fact]
        public async Task ObjectRelationshipSelectCastTest()
        {
            var query = _graphQLClient.QueryBuilder<Staff>()
                .Where(x => x.Subordinates.Any())
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    Subordinates = x.Subordinates.Select(y => new Staff 
                    { 
                        FirstName = y.FirstName,
                        LastName = y.LastName
                    })
                });

            var result = await _graphQLClient.ExecuteAsync(query);

            Assert.Equal(2, result.Count);

            var first = result.First();

            Assert.NotNull(first.FirstName);
            Assert.NotNull(first.LastName);
            Assert.NotNull(first.Subordinates);
            Assert.Equal(3, first.Subordinates.Count());

            var subordinate = first.Subordinates.First();

            Assert.NotNull(subordinate.FirstName);
            Assert.NotNull(subordinate.LastName);
            Assert.Null(subordinate.Email);
            Assert.Null(subordinate.Manager);
            Assert.Null(subordinate.Subordinates);
        }
    }
}
