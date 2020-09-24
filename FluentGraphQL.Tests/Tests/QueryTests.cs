using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using FluentGraphQL.Client.Extensions;
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
            var categoryQ = _graphQLClient.QueryBuilder<Category>().Include(x => x.Products).Build();
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

        [Fact]
        public async Task IncludeNestedEntityTest()
        {
            var queryA = _graphQLClient.QueryBuilder<Category>()
                .Single(x => x.Id == Context.Categories.UrbanBike.Id)
                .Include(x => x.Products)
                .Build();

            var queryB = _graphQLClient.QueryBuilder<Product>()
                .Include(x => x.Category)
                .Include(x => x.Stocks)
                .Build();

            var queryC = _graphQLClient.QueryBuilder<Brand>()
                .Single(x => x.Id == Context.Brands.Cube.Id)
                .Include(x => x.Products.Include(y => y.Category))
                .Build();

            var queryD = _graphQLClient.QueryBuilder<Staff>()
                .Single(x => x.Id == Context.Employees.Lucija.Id)
                .Include(x => x.Manager!.Store)
                .Include(x => x.Manager!.Subordinates)
                .Build();

            var resultA = await _graphQLClient.ExecuteAsync(queryA);
            var resultB = await _graphQLClient.ExecuteAsync(queryB);
            var resultC = await _graphQLClient.ExecuteAsync(queryC);
            var resultD = await _graphQLClient.ExecuteAsync(queryD);

            Assert.NotNull(resultA.Name);
            Assert.NotNull(resultA.Products);

            var product = resultA.Products.First();

            Assert.NotNull(product.Name);
            Assert.Null(product.Category);

            product = resultB.First();

            Assert.NotNull(product.Name);
            Assert.NotNull(product.Category);
            Assert.NotNull(product.Category.Name);
            Assert.NotNull(product.Stocks);
            Assert.Null(product.Brand);
            Assert.Null(product.Category.Products);
            Assert.Null(product.Stocks.First().Store);

            Assert.NotNull(resultC.Name);
            Assert.NotNull(resultC.Products);

            product = resultC.Products.First();

            Assert.NotNull(product.Name);
            Assert.NotNull(product.Category);
            Assert.NotNull(product.Category.Name);
            Assert.Null(product.Stocks);
            Assert.Null(product.Category.Products);

            Assert.NotNull(resultD.FirstName);
            Assert.NotNull(resultD.Manager);
            Assert.NotNull(resultD.Manager!.FirstName);
            Assert.NotNull(resultD.Manager!.Store);
            Assert.NotNull(resultD.Manager!.Store.Name);
            Assert.NotNull(resultD.Manager!.Subordinates);
            Assert.NotNull(resultD.Manager!.Subordinates.First().LastName);
            Assert.Null(resultD.Subordinates);
            Assert.Null(resultD.Store);
            Assert.Null(resultD.Manager.Orders);
            Assert.Null(resultD.Manager.Store.Stocks);
            Assert.Null(resultD.Manager.Subordinates.First().Orders);

            var inserCustomerMutation = _graphQLClient.MutationBuilder<Customer>()
                .Insert(new Customer
                {
                    FirstName = "ABC",
                    LastName = "DEF",
                    Email = "GHI@email.com"
                }).Return(x => x.Id);

            var customerId = await _graphQLClient.ExecuteAsync(inserCustomerMutation);

            var insertOrderMutation = _graphQLClient.MutationBuilder<Order>()
                .Insert(new Order
                {
                    CustomerId = customerId,
                    StoreId = Context.Stores.Zadar.Id,
                    OrderStatusId = Context.OrderStatuses.Pending.Id,
                    StaffId = Context.Employees.Luka.Id,
                    OrderItems = new[]
                    {
                        new OrderItem
                        {
                            ProductId = resultB.First().Id,
                            Price = resultB.First().Price,
                            Quantity = 1
                        }
                    }
                }).Return(x => x.Id);

            var orderId = await _graphQLClient.ExecuteAsync(insertOrderMutation);

             var queryE = _graphQLClient.QueryBuilder<Store>()
                .Single(x => x.Id == Context.Stores.Zadar.Id)
                .Include(x => x.Orders.Include(y => new object[] {
                    y.OrderStatus,
                    y.Staff.Manager!,
                    y.OrderItems.Include(z => z.Product)
                })).Build();

            var resultE = await _graphQLClient.ExecuteAsync(queryE);            

            Assert.NotNull(resultE.Orders);

            var order = resultE.Orders.First();

            Assert.NotNull(resultE.Name);
            Assert.NotNull(order.OrderStatus);
            Assert.NotNull(order.OrderStatus.Name);
            Assert.NotNull(order.Staff);
            Assert.NotNull(order.Staff.FirstName);
            Assert.NotNull(order.Staff.Manager);
            Assert.NotNull(order.Staff.Manager!.Email);
            Assert.NotNull(order.OrderItems);

            Assert.Null(resultE.Stocks);
            Assert.Null(order.Store);
            Assert.Null(order.Customer);
            Assert.Null(order.Staff.Orders);

            var orderItem = order.OrderItems.First();

            Assert.NotNull(orderItem.Product);
            Assert.NotNull(orderItem.Product.Name);

            Assert.Null(orderItem.Order);
            Assert.Null(orderItem.Product.Stocks);
        }

        [Fact]
        public async Task SelectMethodTest()
        {
            var queryA = _graphQLClient.QueryBuilder<Category>()
                .Single(x => x.Id == Context.Categories.TrekkingBike.Id)
                .Select(x => new
                {
                    x.Name,
                    x.Products
                });

            var resultA = await _graphQLClient.ExecuteAsync(queryA);

            Assert.NotNull(resultA.Name);
            Assert.NotNull(resultA.Products);

            var product = resultA.Products.First();

            Assert.NotNull(product.Name);
            Assert.Null(product.Brand);

            var queryB = _graphQLClient.QueryBuilder<Category>()
                .Single(x => x.Id == Context.Categories.RacingBike.Id)
                .Select(x => new
                {
                    x.Name,
                    Products = x.Products.Include(y => y.Brand)
                });

            var resultB = await _graphQLClient.ExecuteAsync(queryB);

            Assert.NotNull(resultB.Products);

            product = resultB.Products.First();

            Assert.NotNull(product.Name);
            Assert.NotNull(product.Brand);
            Assert.NotNull(product.Brand.Name);
            Assert.Null(product.Brand.Products);

            var inserCustomerMutation = _graphQLClient.MutationBuilder<Customer>()
                .Insert(new Customer
                {
                    FirstName = "ABC",
                    LastName = "DEF",
                    Email = "GHI@email.com"
                }).Return(x => x.Id);

            var customerId = await _graphQLClient.ExecuteAsync(inserCustomerMutation);

            var insertOrderMutation = _graphQLClient.MutationBuilder<Order>()
                .Insert(new Order
                {
                    CustomerId = customerId,
                    StoreId = Context.Stores.Zadar.Id,
                    OrderStatusId = Context.OrderStatuses.Pending.Id,
                    StaffId = Context.Employees.Stipe.Id,
                    OrderItems = new[]
                    {
                        new OrderItem
                        {
                            ProductId = product.Id,
                            Price = product.Price,
                            Quantity = 1
                        }
                    }
                }).Return(x => x.Id);

            var orderId = await _graphQLClient.ExecuteAsync(insertOrderMutation);

            var queryC = _graphQLClient.QueryBuilder<Staff>()
                .Single(x => x.Id == Context.Employees.Stipe.Id)
                .Select(x => new
                {
                    x.Email,
                    Manager = x.Manager.Include(y => y!.Store),
                    Orders = x.Orders.Include(y => new
                    {
                        y.OrderStatus,
                        OrderItems = y.OrderItems.Include(z => z.Product)
                    })
                });

            var resultC = await _graphQLClient.ExecuteAsync(queryC);

            Assert.NotNull(resultC.Email);
            Assert.NotNull(resultC.Manager);
            Assert.NotNull(resultC.Manager!.FirstName);
            Assert.NotNull(resultC.Manager!.Store);
            Assert.NotNull(resultC.Manager!.Store.Name);
            Assert.NotNull(resultC.Orders);

            Assert.Null(resultC.Manager.Subordinates);
            Assert.Null(resultC.Manager.Orders);
            Assert.Null(resultC.Manager.Store.Stocks);

            var order = resultC.Orders.First();

            Assert.NotNull(order.OrderStatus);
            Assert.NotNull(order.OrderStatus.Name);
            Assert.NotNull(order.OrderItems);

            Assert.Null(order.Customer);           

            var orderItem = order.OrderItems.First();

            Assert.True(orderItem.Price > 0);
            Assert.NotNull(orderItem.Product);
            Assert.NotNull(orderItem.Product.Name);

            Assert.Null(orderItem.Order);
            Assert.Null(orderItem.Product.Category);
        }
    }
}
