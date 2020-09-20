using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentGraphQL.Tests.Entities
{
    public class Store : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }

        public ICollection<Order> Orders { get; set; } = null!;
        public ICollection<Staff> Staff { get; set; } = null!;
        public ICollection<Stock> Stocks { get; set; } = null!;
    }
}
