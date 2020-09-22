using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Extensions;
using FluentGraphQL.Tests.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentGraphQL.Tests.Infrastructure
{
    public class Context : IDisposable
    {
        public static class Categories
        {
            public static Category MountainBike { get; set; } = null!;
            public static Category CrossBike { get; set; } = null!;
            public static Category ElectricBike { get; set; } = null!;
            public static Category TrekkingBike { get; set; } = null!;
            public static Category RacingBike { get; set; } = null!;
            public static Category UrbanBike { get; set; } = null!;
            public static Category GravelBike { get; set; } = null!;
        }

        public static class Brands
        {
            public static Brand Breezer { get; set; } = null!;
            public static Brand Centurion { get; set; } = null!;
            public static Brand Cervelo { get; set; } = null!;
            public static Brand Cube { get; set; } = null!;
            public static Brand Focus { get; set; } = null!;
            public static Brand Fuji { get; set; } = null!;
            public static Brand Merida { get; set; } = null!;
            public static Brand Mondraker { get; set; } = null!;
            public static Brand Radon { get; set; } = null!;
            public static Brand SantaCruz { get; set; } = null!;
        }

        public static class OrderStatuses
        {
            public static OrderStatus Pending { get; set; } = null!;
            public static OrderStatus Processing { get; set; } = null!;
            public static OrderStatus Rejected { get; set; } = null!;
            public static OrderStatus Completed { get; set; } = null!;
        }

        public static class Stores
        {
            public static Store Zagreb { get; set; } = null!;
            public static Store Zadar { get; set; } = null!;
        }

        public static class Employees
        {
            public static Staff Nikola { get; set; } = null!;
            public static Staff Luka { get; set; } = null!;
            public static Staff Domagoj { get; set; } = null!;
            public static Staff Lucija { get; set; } = null!;

            public static Staff Ivana { get; set; } = null!;
            public static Staff Stipe { get; set; } = null!;
            public static Staff Silvija { get; set; } = null!;
            public static Staff Mate { get; set; } = null!;
        }

        public IGraphQLClient GraphQLClient { get; }
        private bool _disposed;

        public Context()
        {
            GraphQLClient = Configuration.ServiceProvider.GetRequiredService<IGraphQLClient>();
            ClearDatabase();

            var task1 = InsertCategories();
            var task2 = InsertBrands();
            var task3 = InsertOrderStatuses();
            var task4 = InsertStores();

            Task.WaitAll(task1, task2, task3, task4);

            var task5 = InsertStaff();
            var task6 = InsertProducts();

            Task.WaitAll(task5, task6);

            var task7 = InsertStock();

            Task.WaitAll(task7);
        }

        private void ClearDatabase()
        {
            var deleteBrandsMutation = GraphQLClient.MutationBuilder<Brand>().DeleteAll().Return(x => x.Id);
            var deleteCategoriesMutation = GraphQLClient.MutationBuilder<Category>().DeleteAll().Return(x => x.Id);
            var deleteOrderStatusesMutation = GraphQLClient.MutationBuilder<OrderStatus>().DeleteAll().Return(x => x.Id);
            var deleteStoresMutation = GraphQLClient.MutationBuilder<Store>().DeleteAll().Return(x => x.Id);
            var deleteStaffMutation = GraphQLClient.MutationBuilder<Staff>().DeleteAll().Return(x => x.Id);
            var deleteProductsMutation = GraphQLClient.MutationBuilder<Product>().DeleteAll().Return(x => x.Id);
            var deleteStocksMutation = GraphQLClient.MutationBuilder<Stock>().DeleteAll().Return(x => x.ProductId);

            GraphQLClient.ExecuteAsync(deleteStocksMutation).Wait();
            GraphQLClient.ExecuteAsync(deleteProductsMutation).Wait();
            GraphQLClient.ExecuteAsync(deleteStaffMutation).Wait();
            GraphQLClient.ExecuteAsync(deleteStoresMutation).Wait();
            GraphQLClient.ExecuteAsync(deleteOrderStatusesMutation).Wait();
            GraphQLClient.ExecuteAsync(deleteCategoriesMutation).Wait();
            GraphQLClient.ExecuteAsync(deleteBrandsMutation).Wait();
        }

        private Guid MapId(Guid id, string name, IEnumerable<dynamic> collection)
        {
            collection.First(x => x.Name == name).Id = id;
            return id;
        }

        private async Task InsertCategories()
        {
            Categories.ElectricBike = new Category { Name = "Electric Bike" };
            Categories.CrossBike = new Category { Name = "Cross Bike" };
            Categories.GravelBike = new Category { Name = "Gravel Bike" };
            Categories.MountainBike = new Category { Name = "Mountain Bike" };
            Categories.RacingBike = new Category { Name = "Racing Bike" };
            Categories.TrekkingBike = new Category { Name = "Trekking Bike" };
            Categories.UrbanBike = new Category { Name = "Urban Bike" };

            var categories = new[]
            {
                Categories.ElectricBike,
                Categories.CrossBike,
                Categories.GravelBike,
                Categories.MountainBike,
                Categories.RacingBike,
                Categories.TrekkingBike,
                Categories.UrbanBike
            };

            var insertCategoriesMutation = GraphQLClient.MutationBuilder<Category>()
                .Insert(categories).Return(x => MapId(x.Id, x.Name, categories));

            var response = await GraphQLClient.ExecuteAsync(insertCategoriesMutation);
        }

        private async Task InsertBrands()
        {
            Brands.Breezer = new Brand { Name = "Breezer" };
            Brands.Centurion = new Brand { Name = "Centurion" };
            Brands.Cervelo = new Brand { Name = "Cervélo" };
            Brands.Cube = new Brand { Name = "Cube" };
            Brands.Focus = new Brand { Name = "Focus" };
            Brands.Fuji = new Brand { Name = "Fuji" };
            Brands.Merida = new Brand { Name = "Merida" };
            Brands.Mondraker = new Brand { Name = "Mondraker" };
            Brands.Radon = new Brand { Name = "Radon" };
            Brands.SantaCruz = new Brand { Name = "Santa Cruz" };

            var brands = new[]
            {
                Brands.Breezer,
                Brands.Centurion,
                Brands.Cervelo,
                Brands.Cube,
                Brands.Focus,
                Brands.Fuji,
                Brands.Merida,
                Brands.Mondraker,
                Brands.Radon,
                Brands.SantaCruz
            };          

            var insertBrandsMutation = GraphQLClient.MutationBuilder<Brand>()
                .Insert(brands).Return(x => MapId(x.Id, x.Name, brands));

            await GraphQLClient.ExecuteAsync(insertBrandsMutation);
        }

        private async Task InsertOrderStatuses()
        {
            OrderStatuses.Completed = new OrderStatus { Name = "Completed" };
            OrderStatuses.Pending = new OrderStatus { Name = "Pending" };
            OrderStatuses.Processing = new OrderStatus { Name = "Processing" };
            OrderStatuses.Rejected = new OrderStatus { Name = "Rejected" };

            var orderStatuses = new OrderStatus[]
            {
                OrderStatuses.Completed,
                OrderStatuses.Pending,
                OrderStatuses.Processing,
                OrderStatuses.Rejected
            };

            var insertOrderStatusesMutation = GraphQLClient.MutationBuilder<OrderStatus>()
                .Insert(orderStatuses).Return(x => MapId(x.Id, x.Name, orderStatuses));

            await GraphQLClient.ExecuteAsync(insertOrderStatusesMutation);
        }

        private async Task InsertStores()
        {
            Stores.Zagreb = new Store
            {
                City = "Zagreb",
                Email = "zagreb-store@bike-sport.com",
                Name = "Bike Sport #1",
                ZipCode = "10000"
            };

            Stores.Zadar = new Store
            {
                City = "Zadar",
                Email = "zadar-store@bike-sport.com",
                Name = "Bike Sport #2",
                ZipCode = "23000"
            };            

            var stores = new Store[]
            {
                Stores.Zagreb,
                Stores.Zadar
            };

            var insertStoresMutation = GraphQLClient.MutationBuilder<Store>()
                .Insert(stores).Return(x => MapId(x.Id, x.Name, stores));

            await GraphQLClient.ExecuteAsync(insertStoresMutation);
        }

        private async Task InsertStaff()
        {
            var zagrebStoreQuery = GraphQLClient.QueryBuilder<Store>()
               .Single(x => x.Name == "Bike Sport #1")
               .Select(x => x.Id);

            var zadarStoreQuery = GraphQLClient.QueryBuilder<Store>()
               .Single(x => x.Name == "Bike Sport #2")
               .Select(x => x.Id);

            var zagrebStoreId = await GraphQLClient.ExecuteAsync(zagrebStoreQuery);
            var zadarStoreId = await GraphQLClient.ExecuteAsync(zadarStoreQuery);

            Employees.Nikola = new Staff
            {
                Active = true,
                Email = "nikola.horvat@bike-store.com",
                FirstName = "Nikola",
                LastName = "Horvat",
                StoreId = zagrebStoreId
            };

            Employees.Luka = new Staff
            {
                Active = true,
                Email = "luka.knezevic@bike-store.com",
                FirstName = "Luka",
                LastName = "Knežević",
                StoreId = zagrebStoreId
            };

            Employees.Domagoj = new Staff
            {
                Active = true,
                Email = "domagoj.kovacevic@bike-store.com",
                FirstName = "Domagoj",
                LastName = "Kovačević",
                StoreId = zagrebStoreId
            };

            Employees.Lucija = new Staff
            {
                Active = true,
                Email = "lucija.pavlovic@bike-store.com",
                FirstName = "Lucija",
                LastName = "Pavlović",
                StoreId = zagrebStoreId
            };

            Employees.Ivana = new Staff
            {
                Active = true,
                Email = "ivana.blazevic@bike-store.com",
                FirstName = "Ivana",
                LastName = "Blažević",
                StoreId = zadarStoreId
            };

            Employees.Stipe = new Staff
            {
                Active = true,
                Email = "stipe.bozic@bike-store.com",
                FirstName = "Stipe",
                LastName = "Božić",
                StoreId = zadarStoreId
            };

            Employees.Silvija = new Staff
            {
                Active = true,
                Email = "silvija.lovric@bike-store.com",
                FirstName = "Silvija",
                LastName = "Lovrić",
                StoreId = zadarStoreId
            };

            Employees.Mate = new Staff
            {
                Active = true,
                Email = "mate.babic@bike-store.com",
                FirstName = "Mate",
                LastName = "Babić",
                StoreId = zadarStoreId
            };

            var zagrebStaff = new Staff[]
            {
                Employees.Nikola,
                Employees.Luka,
                Employees.Domagoj,
                Employees.Lucija
            };

            var zadarStaff = new Staff[]
            {
                Employees.Ivana,
                Employees.Stipe,
                Employees.Silvija,
                Employees.Mate
            };

            Func<Guid, string, Staff[], Guid> mapId = (id, firstName, collection) => 
            {
                collection.First(x => x.FirstName == firstName).Id = id;
                return id;
            };

            Func<Guid, Staff, Staff[], Guid> mapManagerId = (id, manager, collection) =>
            {
                var employee = collection.First(x => x.Id == id);
                employee.ManagerId = manager.Id;
                employee.Manager = manager;

                return manager.Id;
            };

            var insertZagrebStaffMutation = GraphQLClient.MutationBuilder<Staff>()
                .Insert(zagrebStaff)
                .Return(x => mapId.Invoke(x.Id, x.FirstName, zagrebStaff));

            var insertZadarStaffMutation = GraphQLClient.MutationBuilder<Staff>()
                .Insert(zadarStaff)
                .Return(x => mapId.Invoke(x.Id, x.FirstName, zadarStaff));

            var insertZagrebStaffResult = await GraphQLClient.ExecuteAsync(insertZagrebStaffMutation);
            var insertZadarStaffResult = await GraphQLClient.ExecuteAsync(insertZadarStaffMutation);

            var setZagrebManagerMutation = GraphQLClient.MutationBuilder<Staff>()
                .UpdateWhere(x => x.Id.In(insertZagrebStaffResult.Returning.TakeLast(3)))
                .Set(x => x.ManagerId, insertZagrebStaffResult.Returning.First())
                .Return(x => mapManagerId(x.Id, x.Manager!, zagrebStaff));

            var setZadarManagerMutation = GraphQLClient.MutationBuilder<Staff>()
                .UpdateWhere(x => x.Id.In(insertZadarStaffResult.Returning.TakeLast(3)))
                .Set(x => x.ManagerId, insertZadarStaffResult.Returning.First())
                .Return(x => mapManagerId(x.Id, x.Manager!, zadarStaff));

            await GraphQLClient.ExecuteAsync(setZagrebManagerMutation);
            await GraphQLClient.ExecuteAsync(setZadarManagerMutation);
        }

        private async Task InsertProducts()
        {
            var products = new Product[]
            {
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 839.29M,
                    Name = "Attention SL black´n´blue"                    
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 855.25M,
                    Name = "Attention grey´n´green"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 471.64M,
                    Name = "Aim Race red´n´orange"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 4200.63M,
                    Name = "Stereo 150 C:68 TM 29 grey´n´orange"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 3570.38M,
                    Name = "Stereo 150 C:62 SL 29 actionteam"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 692.23M,
                    Name = "Access WS Race grey´n´lime"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 5881.30M,
                    Name = "AMS 100 C:68 SLT 29 carbon´n´blue"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 4620.80M,
                    Name = "Stereo 150 C:68 SLT 29 carbon´n´red"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 3885.50M,
                    Name = "SUMMUM CARBON PRO"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 6091.39M,
                    Name = "FOXY CARBON RR 29 SE"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 4200.63M,
                    Name = "FOXY CARBON XR 27.5"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 5776.26M,
                    Name = "SUMMUM CARBON PRO TEAM"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 3402.31M,
                    Name = "Jealous 9.0"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 4200.63M,
                    Name = "Jealous 10.0"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 2835.08M,
                    Name = "Jealous 8.0"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 944.33M,
                    Name = "ZR Lady 8.0"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 5251.05M,
                    Name = "Megatower CS"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 6721.64M,
                    Name = "Megatower CC X01"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 4830.88M,
                    Name = "Hightower CS"
                },
                new Product
                {
                    CategoryId = Categories.MountainBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 7036.76M,
                    Name = "Hightower CC X01 RSV"
                },
                new Product
                {
                    CategoryId = Categories.RacingBike.Id,
                    BrandId = Brands.Cervelo.Id,
                    ModelYear = 2020,
                    Price = 5671.22M,
                    Name = "S3 Disc Force Etap AXS"
                },
                new Product
                {
                    CategoryId = Categories.RacingBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 1784.66M,
                    Name = "Axial WS GTC SL carbon´n´hazypurple"
                },
                new Product
                {
                    CategoryId = Categories.RacingBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 2309.87M,
                    Name = "Aerium Race carbon`n`blue"
                },
                new Product
                {
                    CategoryId = Categories.RacingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 2309.87M,
                    Name = "Vaillant Disc 9.0"
                },
                new Product
                {
                    CategoryId = Categories.RacingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 1889.71M,
                    Name = "Vaillant Disc 8.0"
                },
                new Product
                {
                    CategoryId = Categories.RacingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 1049.37M,
                    Name = "R1 Disc Tiagra"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 4200.63M,
                    Name = "Agree Hybrid C:62 SL carbon´n´red"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 4200.63M,
                    Name = "Agree Hybrid C:62 SL carbon´n´red"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 3990.55M,
                    Name = "Nuroad Hybrid C:62 SL blue´n´blue"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 3150.21M,
                    Name = "Agree Hybrid C:62 Race carbon´n´white"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 6461.13M,
                    Name = "Agree Hybrid C:62 SLT black edition"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 2730.04M,
                    Name = "Access Hybrid EX 625 Allroad 29 titan´n´berry"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Focus.Id,
                    ModelYear = 2020,
                    Price = 4095.59M,
                    Name = "JAM² C Plus Exclusive Model"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Focus.Id,
                    ModelYear = 2020,
                    Price = 3780.46M,
                    Name = "PARALANE² 9.7"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Focus.Id,
                    ModelYear = 2020,
                    Price = 3150.21M,
                    Name = "PARALANE² 6.9"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 5251.05M,
                    Name = "LEVEL RR KIOX"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 5040.97M,
                    Name = "DUSK RR 29"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.Mondraker.Id,
                    ModelYear = 2020,
                    Price = 4725.84M,
                    Name = "DUSK R 29"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 10293.07M,
                    Name = "Heckler CC XX1 AXS RSV"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 8402.31M,
                    Name = "Heckler CC X01 RSV"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 5881.30M,
                    Name = "Heckler CC R"
                },
                new Product
                {
                    CategoryId = Categories.ElectricBike.Id,
                    BrandId = Brands.SantaCruz.Id,
                    ModelYear = 2020,
                    Price = 6464.29M,
                    Name = "Heckler CC S"
                },
                new Product
                {
                    CategoryId = Categories.TrekkingBike.Id,
                    BrandId = Brands.Breezer.Id,
                    ModelYear = 2020,
                    Price = 713.24M,
                    Name = "Liberty IGR+ LS"
                },
                new Product
                {
                    CategoryId = Categories.TrekkingBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 1574.58M,
                    Name = "Kathmandu SL black´n´green"
                },
                new Product
                {
                    CategoryId = Categories.TrekkingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 1101.89M,
                    Name = "Sunset 9.0"
                },
                new Product
                {
                    CategoryId = Categories.TrekkingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 1469.54M,
                    Name = "Sunset Supreme"
                },
                new Product
                {
                    CategoryId = Categories.TrekkingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 1364.50M,
                    Name = "Sunset 10.0"
                },
                new Product
                {
                    CategoryId = Categories.TrekkingBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 1259.45M,
                    Name = "Sunset 9.0 Lady"
                },
                new Product
                {
                    CategoryId = Categories.UrbanBike.Id,
                    BrandId = Brands.Centurion.Id,
                    ModelYear = 2020,
                    Price = 891.81M,
                    Name = "City Speed 500 Tour"
                },
                new Product
                {
                    CategoryId = Categories.UrbanBike.Id,
                    BrandId = Brands.Centurion.Id,
                    ModelYear = 2020,
                    Price = 891.81M,
                    Name = "City Speed 8 Tour"
                },
                new Product
                {
                    CategoryId = Categories.UrbanBike.Id,
                    BrandId = Brands.Fuji.Id,
                    ModelYear = 2020,
                    Price = 419.12M,
                    Name = "Declaration"
                },
                new Product
                {
                    CategoryId = Categories.UrbanBike.Id,
                    BrandId = Brands.Fuji.Id,
                    ModelYear = 2020,
                    Price = 545.17M,
                    Name = "Feather"
                },
                new Product
                {
                    CategoryId = Categories.UrbanBike.Id,
                    BrandId = Brands.Breezer.Id,
                    ModelYear = 2020,
                    Price = 1889.71M,
                    Name = "Inversion Pro"
                },
                new Product
                {
                    CategoryId = Categories.UrbanBike.Id,
                    BrandId = Brands.Fuji.Id,
                    ModelYear = 2020,
                    Price = 2414.92M,
                    Name = "Jari Carbon 1.3"
                },
                new Product
                {
                    CategoryId = Categories.CrossBike.Id,
                    BrandId = Brands.Merida.Id,
                    ModelYear = 2020,
                    Price = 1049.37M,
                    Name = "Crossway Urban XT-Edition"
                },
                new Product
                {
                    CategoryId = Categories.CrossBike.Id,
                    BrandId = Brands.Cube.Id,
                    ModelYear = 2020,
                    Price = 1417.02M,
                    Name = "Editor black´n´purple"
                },
                new Product
                {
                    CategoryId = Categories.CrossBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 944.33M,
                    Name = "Scart Light 9.0"
                },
                new Product
                {
                    CategoryId = Categories.CrossBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 660.71M,
                    Name = "Scart Light 8.0"
                },
                new Product
                {
                    CategoryId = Categories.CrossBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 839.29M,
                    Name = "Scart Light 9.0 Lady"
                },
                new Product
                {
                    CategoryId = Categories.CrossBike.Id,
                    BrandId = Brands.Radon.Id,
                    ModelYear = 2020,
                    Price = 660.71M,
                    Name = "Scart Light 8.0 Lady"
                }
            };

            var insertProductsMutation = GraphQLClient.MutationBuilder<Product>()
                .Insert(products)
                .Return(x => x.Id);

            var result = await GraphQLClient.ExecuteAsync(insertProductsMutation);
        }        

        private async Task InsertStock()
        {
            var productQuery = GraphQLClient.QueryBuilder<Product>()
                .Select(x => x.Id);

            var stores = new[] { Stores.Zadar, Stores.Zagreb }.Select(x => x.Id).ToArray();
            var products = await GraphQLClient.ExecuteAsync(productQuery);
            var tasks = new ConcurrentBag<Task>();

            Parallel.ForEach(products, (product) =>
            {
                foreach (var store in stores)
                {
                    var quantity = new Random().Next(1, 20);
                    var insertStockMutation = GraphQLClient.MutationBuilder<Stock>()
                    .Insert(new Stock
                    {
                        ProductId = product,
                        StoreId = store,
                        Quantity = quantity
                    }).Return(x => x.ProductId);

                    tasks.Add(GraphQLClient.ExecuteAsync(insertStockMutation));
                }
            });

            await Task.WhenAll(tasks);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    ClearDatabase();

                _disposed = true;
            }
        }
      
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
