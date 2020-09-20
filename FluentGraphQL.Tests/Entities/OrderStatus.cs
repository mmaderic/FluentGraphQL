using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentGraphQL.Tests.Entities
{
    public class OrderStatus : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        ICollection<Order> Orders { get; set; } = null!;
    }
}
