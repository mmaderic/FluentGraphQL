using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentGraphQL.Tests.Entities
{
    public class Staff : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Active { get; set; }
        public Guid StoreId { get; set; }
        public Guid ManagerId { get; set; }

        public string? Phone { get; set; }

        public Staff Manager { get; set; } = null!;
        public Store Store { get; set; } = null!;

        public ICollection<Staff> Subordinates { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
