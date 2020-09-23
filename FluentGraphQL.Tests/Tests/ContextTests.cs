using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Tests.Entities;
using FluentGraphQL.Tests.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentGraphQL.Tests.Tests
{
    [Collection("Context collection")]
    public class ContextTests
    {
        private readonly IGraphQLClient _graphQLClient;

        public ContextTests(Context context)
        {
            _graphQLClient = context.GraphQLClient;
        }

        [Fact]
        public async Task CategoryTest()
        {
            var categoriesQ = _graphQLClient.QueryBuilder<Category>().Select(x => new
            {
                x.Id,
                x.Name
            });

            var categoriesR = await _graphQLClient.ExecuteAsync(categoriesQ);

            Assert.Equal(7, categoriesR.Count);

            var crossBike = categoriesR.First(y => y.Id == Context.Categories.CrossBike.Id);
            var electricBike = categoriesR.First(y => y.Id == Context.Categories.ElectricBike.Id);
            var gravelBike = categoriesR.First(y => y.Id == Context.Categories.GravelBike.Id);
            var mountainBike = categoriesR.First(y => y.Id == Context.Categories.MountainBike.Id);
            var racingBike = categoriesR.First(y => y.Id == Context.Categories.RacingBike.Id);
            var trekkingBike = categoriesR.First(y => y.Id == Context.Categories.TrekkingBike.Id);
            var urbanBike = categoriesR.First(y => y.Id == Context.Categories.UrbanBike.Id);

            Assert.Equal(Context.Categories.CrossBike.Name, crossBike.Name);
            Assert.Equal(Context.Categories.ElectricBike.Name, electricBike.Name);
            Assert.Equal(Context.Categories.GravelBike.Name, gravelBike.Name);
            Assert.Equal(Context.Categories.MountainBike.Name, mountainBike.Name);
            Assert.Equal(Context.Categories.RacingBike.Name, racingBike.Name);
            Assert.Equal(Context.Categories.TrekkingBike.Name, trekkingBike.Name);
            Assert.Equal(Context.Categories.UrbanBike.Name, urbanBike.Name);
        }

        [Fact]
        public async Task BrandTest()
        {
            var brandsQ = _graphQLClient.QueryBuilder<Brand>().Select(x => new
            {
                x.Id,
                x.Name
            });

            var brandsR = await _graphQLClient.ExecuteAsync(brandsQ);

            Assert.Equal(10, brandsR.Count);

            var breezer = brandsR.First(y => y.Id == Context.Brands.Breezer.Id);
            var centurion = brandsR.First(y => y.Id == Context.Brands.Centurion.Id);
            var cervelo = brandsR.First(y => y.Id == Context.Brands.Cervelo.Id);
            var cube = brandsR.First(y => y.Id == Context.Brands.Cube.Id);
            var focus = brandsR.First(y => y.Id == Context.Brands.Focus.Id);
            var fuji = brandsR.First(y => y.Id == Context.Brands.Fuji.Id);
            var merida = brandsR.First(y => y.Id == Context.Brands.Merida.Id);
            var mondraker = brandsR.First(y => y.Id == Context.Brands.Mondraker.Id);
            var radon = brandsR.First(y => y.Id == Context.Brands.Radon.Id);
            var santaCruz = brandsR.First(y => y.Id == Context.Brands.SantaCruz.Id);

            Assert.Equal(Context.Brands.Breezer.Name, breezer.Name);
            Assert.Equal(Context.Brands.Centurion.Name, centurion.Name);
            Assert.Equal(Context.Brands.Cervelo.Name, cervelo.Name);
            Assert.Equal(Context.Brands.Cube.Name, cube.Name);
            Assert.Equal(Context.Brands.Focus.Name, focus.Name);
            Assert.Equal(Context.Brands.Fuji.Name, fuji.Name);
            Assert.Equal(Context.Brands.Merida.Name, merida.Name);
            Assert.Equal(Context.Brands.Mondraker.Name, mondraker.Name);
            Assert.Equal(Context.Brands.Radon.Name, radon.Name);
            Assert.Equal(Context.Brands.SantaCruz.Name, santaCruz.Name);
        }

        [Fact]
        public async Task OrderStatusTest()
        {
            var orderStatusQ = _graphQLClient.QueryBuilder<OrderStatus>()
            .Select(x => new
            {
                x.Id,
                x.Name
            });

            var orderStatusR = await _graphQLClient.ExecuteAsync(orderStatusQ);

            Assert.Equal(4, orderStatusR.Count);

            var completed = orderStatusR.First(y => y.Id == Context.OrderStatuses.Completed.Id);
            var pending = orderStatusR.First(y => y.Id == Context.OrderStatuses.Pending.Id);
            var processing = orderStatusR.First(y => y.Id == Context.OrderStatuses.Processing.Id);
            var rejected = orderStatusR.First(y => y.Id == Context.OrderStatuses.Rejected.Id);

            Assert.Equal(Context.OrderStatuses.Completed.Name, completed.Name);
            Assert.Equal(Context.OrderStatuses.Pending.Name, pending.Name);
            Assert.Equal(Context.OrderStatuses.Processing.Name, processing.Name);
            Assert.Equal(Context.OrderStatuses.Rejected.Name, rejected.Name);
        }

        [Fact]
        public async Task StoreTest()
        {
            var storeQ = _graphQLClient.QueryBuilder<Store>()
            .Select(x => new
            {
                x.Id,
                x.Name
            });

            var storeR = await _graphQLClient.ExecuteAsync(storeQ);

            Assert.Equal(2, storeR.Count);

            var zadar = storeR.First(y => y.Id == Context.Stores.Zadar.Id);
            var zagreb = storeR.First(y => y.Id == Context.Stores.Zagreb.Id);

            Assert.Equal(Context.Stores.Zadar.Name, zadar.Name);
            Assert.Equal(Context.Stores.Zagreb.Name, zagreb.Name);
        }

        [Fact]
        public async Task StaffTest()
        {
            var staffQ = _graphQLClient.QueryBuilder<Staff>()
            .Select(x => new
            {
                x.Id,
                x.FirstName,
                x.ManagerId
            });

            var staffR = await _graphQLClient.ExecuteAsync(staffQ);

            Assert.Equal(8, staffR.Count);

            var domagoj = staffR.First(y => y.Id == Context.Employees.Domagoj.Id);
            var ivana = staffR.First(y => y.Id == Context.Employees.Ivana.Id);
            var lucija = staffR.First(y => y.Id == Context.Employees.Lucija.Id);
            var luka = staffR.First(y => y.Id == Context.Employees.Luka.Id);
            var mate = staffR.First(y => y.Id == Context.Employees.Mate.Id);
            var nikola = staffR.First(y => y.Id == Context.Employees.Nikola.Id);
            var silvija = staffR.First(y => y.Id == Context.Employees.Silvija.Id);
            var stipe = staffR.First(y => y.Id == Context.Employees.Stipe.Id);

            Assert.Equal(Context.Employees.Domagoj.FirstName, domagoj.FirstName);
            Assert.Equal(Context.Employees.Domagoj.ManagerId, domagoj.ManagerId);

            Assert.Equal(Context.Employees.Ivana.FirstName, ivana.FirstName);
            Assert.Equal(Context.Employees.Ivana.ManagerId, ivana.ManagerId);

            Assert.Equal(Context.Employees.Lucija.FirstName, lucija.FirstName);
            Assert.Equal(Context.Employees.Lucija.ManagerId, lucija.ManagerId);

            Assert.Equal(Context.Employees.Luka.FirstName, luka.FirstName);
            Assert.Equal(Context.Employees.Luka.ManagerId, luka.ManagerId);

            Assert.Equal(Context.Employees.Mate.FirstName, mate.FirstName);
            Assert.Equal(Context.Employees.Mate.ManagerId, mate.ManagerId);

            Assert.Equal(Context.Employees.Nikola.FirstName, nikola.FirstName);
            Assert.Equal(Context.Employees.Nikola.ManagerId, nikola.ManagerId);

            Assert.Equal(Context.Employees.Silvija.FirstName, silvija.FirstName);
            Assert.Equal(Context.Employees.Silvija.ManagerId, silvija.ManagerId);

            Assert.Equal(Context.Employees.Stipe.FirstName, stipe.FirstName);
            Assert.Equal(Context.Employees.Stipe.ManagerId, stipe.ManagerId);
        }

        [Fact]
        public async Task ProductTest()
        {
            var productQ = _graphQLClient.QueryBuilder<Product>()
                .Aggregate()
                    .Count()
                .Build();

            var productR = await _graphQLClient.ExecuteAsync(productQ);
            var count = productR.Aggregate.Count;

            Assert.Equal(60, count);
        }

        [Fact]
        public async Task StockTest()
        {
            var stockQA = _graphQLClient.QueryBuilder<Stock>()
                .Where(x => x.StoreId == Context.Stores.Zadar.Id)
                .Aggregate()
                    .Count()
                .Build();

            var stockQB = _graphQLClient.QueryBuilder<Stock>()
                .Where(x => x.StoreId == Context.Stores.Zagreb.Id)
                .Aggregate()
                    .Count()
                .Build();

            var stockRA = await _graphQLClient.ExecuteAsync(stockQA);
            var stockRB = await _graphQLClient.ExecuteAsync(stockQB);

            var countA = stockRA.Aggregate.Count;
            var countB = stockRB.Aggregate.Count;

            Assert.Equal(60, countA);
            Assert.Equal(60, countB);
        }
    }
}
