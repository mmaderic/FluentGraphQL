using FluentGraphQL.Client;
using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    [Collection("Context collection")]
    public class CombinedTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public CombinedTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task CaseOneTest()
        {
            var insertCustomerMutation = _graphQLClient.MutationBuilder<Customer>()
                .Insert(new Customer
                {
                    FirstName = "test",
                    LastName = "customer",
                    Email = "some@email.com"
                }).Return(x => x.Id);

            var productQuery = _graphQLClient.QueryBuilder<Product>()
                .Where(x =>
                    x.BrandId == Context.Brands.Cube.Id &&
                    x.CategoryId == Context.Categories.MountainBike.Id &&
                    x.Stocks.Any(y => y.StoreId == Context.Stores.Zadar.Id && y.Quantity > 0))
                .Node<Stock>()
                    .Where(x => x.StoreId == Context.Stores.Zadar.Id)
                .Select(x => new
                {
                    x.Id,
                    x.Price,
                    x.Stocks
                });

            var customerId = await _graphQLClient.ExecuteAsync(insertCustomerMutation);
            var products = await _graphQLClient.ExecuteAsync(productQuery);
            
            Assert.True(products.Count > 0);
            var product = products.First();

            Assert.True(product.Stocks.Count == 1);
            var stock = product.Stocks.First();

            var insertOrderMutation = _graphQLClient.MutationBuilder<Order>()
                .Insert(new Order
                {
                    CustomerId = customerId,
                    OrderStatusId = Context.OrderStatuses.Pending.Id,
                    StaffId = Context.Employees.Mate.Id,
                    StoreId = Context.Stores.Zadar.Id,
                    OrderItems = new OrderItem[]
                    {
                        new OrderItem
                        {
                            ProductId = product.Id,
                            Price = product.Price,
                            Quantity = 1
                        }
                    }
                }).Return(x => x.Id);

            var decreaseStockQuantityMutation = _graphQLClient.MutationBuilder<Stock>()
                .UpdateWhere(x =>
                    x.StoreId == Context.Stores.Zadar.Id &&
                    x.ProductId == product.Id &&
                    x.Quantity == stock.Quantity)
                .Set(x => x.Quantity, stock.Quantity - 1)
                .Return(x => x.Quantity);

            var transaction = GraphQLTransaction.Construct(insertOrderMutation, decreaseStockQuantityMutation);
            var result = await _graphQLClient.ExecuteAsync(transaction);

            var confirmationQuery = _graphQLClient.QueryBuilder<Order>()
                .ByPrimaryKey(x => x.Id, result.First)
                .Build();

            var confirmationResult = await _graphQLClient.ExecuteAsync(confirmationQuery);

            Assert.Equal(1, confirmationResult.OrderItems.Count);
            Assert.Equal(1, result.Second.AffectedRows);
            Assert.Equal(stock.Quantity - 1, result.Second.First());            
        }
    }
}
