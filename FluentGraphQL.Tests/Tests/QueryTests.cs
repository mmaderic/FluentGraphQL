﻿using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using FluentGraphQL.Client.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System;

namespace FluentGraphQL.Tests
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

            var queryD = _graphQLClient.QueryBuilder<Stock>()
                .Select(x => new
                {
                    Id = x.StoreId,
                    Product = x.Product.Id,
                    Customers = x.Product.OrderItems.Select(y => new
                    {
                        y.Order.Customer.Email
                    })
                });

            var resultD = await _graphQLClient.ExecuteAsync(queryD);
            var stock = resultD.Single(x => x.Id == Context.Stores.Zadar.Id && x.Product == product.Id);

            Assert.Contains(stock.Customers, x => x.Email == "GHI@email.com");

            var queryE = _graphQLClient.QueryBuilder<Store>()
                .Select(x => new
                {
                    Store = x.Name,
                    Sellers = x.Orders.Select(y => new
                    {
                        Employee = y.OrderStatus.Orders.Select(z => z.Staff.FirstName)
                    })
                });

            var resultE = await _graphQLClient.ExecuteAsync(queryE);
            var store = resultE.Single(x => x.Store == Context.Stores.Zadar.Name);
            var employees = store.Sellers.SelectMany(x => x.Employee);

            Assert.Contains(employees, x => x == "Stipe");

            var queryF = _graphQLClient.QueryBuilder<Store>()
                .Select(x => new
                {
                    Store = x.Name,
                    Sellers = x.Orders.Select(y => y.OrderStatus.Orders.Select(z => z.Staff.FirstName))
                });

            var resultF = await _graphQLClient.ExecuteAsync(queryF);
            var store2 = resultF.Single(x => x.Store == Context.Stores.Zadar.Name);
            var sellers = store2.Sellers.SelectMany(x => x);

            Assert.Contains(sellers, x => x == "Stipe");
            Assert.Equal(queryE.QueryString, queryF.QueryString);

            var queryG = _graphQLClient.QueryBuilder<Store>()
                .Select(x => new
                {
                    x.Name,
                    Orders = x.Orders.Select(y => y.OrderStatus.Orders.Select(z => z.Staff.FirstName))
                }).Cast();

            var resultG = await _graphQLClient.ExecuteAsync(queryG);
            var store3 = resultG.Single(x => x.Name == Context.Stores.Zadar.Name);
            var staff = store3.Orders.SelectMany(x => x.OrderStatus.Orders).Select(x => x.Staff.FirstName);

            Assert.Contains(staff, x => x == "Stipe");
            Assert.Equal(queryF.QueryString, queryG.QueryString);

            var queryH = _graphQLClient.QueryBuilder<Brand>()
                .Select(x => new
                {
                    x.Name,
                    Products = x.Products.Select(y => y.Category.Select(z => z.Name))
                }).Cast();

            var resultH = await _graphQLClient.ExecuteAsync(queryH);
            var brand = resultH.First();

            Assert.Equal(Guid.Empty, brand.Id);
            Assert.NotNull(brand.Name);
            Assert.NotNull(brand.Products);

            var product2 = brand.Products.First();

            Assert.Equal(Guid.Empty, product2.Id);
            Assert.Null(product2.Name);
            Assert.Null(product2.OrderItems);
            Assert.NotNull(product2.Category);
            Assert.Equal(Guid.Empty, product2.Category.Id);
            Assert.NotNull(product2.Category.Name);

            var queryI = _graphQLClient.QueryBuilder<Staff>()
                .ByPrimaryKey(x => x.Id, Context.Employees.Domagoj.Id)
                .Select(x => x.Manager.Select(y => new
                {
                    y!.FirstName,
                    y!.LastName
                })).Cast();

            var resultI = await _graphQLClient.ExecuteAsync(queryI);

            Assert.Null(resultI.LastName);
            Assert.Null(resultI.LastName);
            Assert.Null(resultI.Store);
            Assert.NotNull(resultI.Manager);
            Assert.NotNull(resultI.Manager!.FirstName);
            Assert.NotNull(resultI.Manager!.LastName);
            Assert.Null(resultI.Manager.Email);
        }

        [Fact]
        public async Task AggregateContainerTests()
        {
            var queryA = _graphQLClient.QueryBuilder<Product>()
                .Aggregate()
                    .Max(x => x.Price)
                .Build();

            var resultA = await _graphQLClient.ExecuteAsync(queryA);
            var maxPrice = resultA.Max(x => x.Price);

            Assert.Equal(10293.07M, maxPrice);

            var queryB = _graphQLClient.QueryBuilder<Product>()
                .Aggregate()
                    .Max<object>(x => new object[] { x.ModelYear, x.Price })
                .Build();

            var resultB = await _graphQLClient.ExecuteAsync(queryB);
            var maxYear = resultB.Max(x => x.ModelYear);
            var maxPrice2 = resultB.Max(x => x.Price);

            Assert.Equal(10293.07M, maxPrice2);
            Assert.Equal(2020, maxYear);

            var queryC = _graphQLClient.QueryBuilder<Staff>()
                .Aggregate()
                    .Max(x => x.FirstName)
                    .Min(x => x.LastName)
                .Build();

            var resultC = await _graphQLClient.ExecuteAsync(queryC);

            Assert.Equal("Stipe", resultC.Max(x => x.FirstName));
            Assert.Equal("Babić", resultC.Min(X => X.LastName));

            var queryD = _graphQLClient.QueryBuilder<Store>()
                .Aggregate<Order>()
                    .Max(x => x.CreatedAt)
                .Build();

            var resultD = await _graphQLClient.ExecuteAsync(queryD);
        }             

        [Fact]
        public async Task DistinctTests()
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
    }
}
