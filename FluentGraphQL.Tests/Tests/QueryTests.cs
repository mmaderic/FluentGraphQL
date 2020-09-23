using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    [Collection("Context collection")]
    public class QueryTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public QueryTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task QueryListOfObjectsTest()
        {
            var categoryQ = _graphQLClient.QueryBuilder<Category>().Build();
            var categoryR = await _graphQLClient.ExecuteAsync(categoryQ);

            Assert.Equal(typeof(List<Category>), categoryR.GetType());
        }

        [Fact]
        public async Task QueryByPrimaryKeyTest()
        {
            var queryA = _graphQLClient.QueryBuilder<Brand>().ById(Context.Brands.Breezer.Id).Build();
            var queryB = _graphQLClient.QueryBuilder<Brand>().ByPrimaryKey("Id", Context.Brands.Breezer.Id).Build();
            var queryC = _graphQLClient.QueryBuilder<Brand>().ByPrimaryKey(x => x.Id, Context.Brands.Breezer.Id).Build();

            var resultA = await _graphQLClient.ExecuteAsync(queryA);
            var resultB = await _graphQLClient.ExecuteAsync(queryB);
            var resultC = await _graphQLClient.ExecuteAsync(queryC);

            Assert.Equal(typeof(Brand), resultA.GetType());
            Assert.Equal(typeof(Brand), resultB.GetType());
            Assert.Equal(typeof(Brand), resultC.GetType());

            Assert.Equal(Context.Brands.Breezer.Id, resultA.Id);
            Assert.Equal(Context.Brands.Breezer.Id, resultB.Id);
            Assert.Equal(Context.Brands.Breezer.Id, resultC.Id);
        }

        [Fact]
        public async Task FilteringQueriesTest()
        {
            var queryA = _graphQLClient.QueryBuilder<Staff>()
                .Where(x => x.ManagerId == null)
                .Build();

            var queryB = _graphQLClient.QueryBuilder<Staff>()
                .Single(x => x.ManagerId == null && x.StoreId == Context.Stores.Zagreb.Id)
                .Build();

            var resultA = await _graphQLClient.ExecuteAsync(queryA);   
            var resultB = await _graphQLClient.ExecuteAsync(queryB);

            Assert.Equal(2, resultA.Count);
            Assert.Equal(Context.Employees.Nikola.Id, resultB.Id);
        }

        [Fact]
        public async Task FilteringQueriesByNestedEntityTest()
        {
            var productQ = _graphQLClient.QueryBuilder<Product>()
                .Where(x =>
                    x.CategoryId == Context.Categories.ElectricBike.Id &&
                    x.Stocks.Any(y => y.StoreId == Context.Stores.Zagreb.Id && y.Quantity > 0) &&
                    (x.BrandId == Context.Brands.Focus.Id || x.BrandId == Context.Brands.Mondraker.Id))
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Brand = x.Brand.Name        
                });
         
            var productR = await _graphQLClient.ExecuteAsync(productQ);

            Assert.Equal(6, productR.Count);
        }
    }
}
