using FluentGraphQL.Builder.Abstractions;
using System;

namespace FluentGraphQL.Tests.Entities
{
    public class Stock : IGraphQLEntity
    {
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Store Store { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
