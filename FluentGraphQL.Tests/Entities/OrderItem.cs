using FluentGraphQL.Builder.Abstractions;
using System;

namespace FluentGraphQL.Tests.Entities
{
    public class OrderItem : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
