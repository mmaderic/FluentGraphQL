using FluentGraphQL.Builder.Abstractions;
using System;

namespace FluentGraphQL.Tests.Entities
{
    public class Product : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public int ModelYear { get; set; }
        public decimal ListPrice { get; set; }

        public Brand Brand { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
