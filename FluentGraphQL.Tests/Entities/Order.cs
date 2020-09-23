using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentGraphQL.Tests.Entities
{
    public class Order : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OrderStatusId { get; set; }
        public Guid StaffId { get; set; }
        public Guid StoreId { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? ShippedDate { get; set; }

        public Customer Customer { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; } = null!;
        public Staff Staff { get; set; } = null!;
        public Store Store { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}
