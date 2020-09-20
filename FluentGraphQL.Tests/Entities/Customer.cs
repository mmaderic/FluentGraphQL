using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentGraphQL.Tests.Entities
{
    public class Customer : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        public ICollection<Order> Orders { get; set; } = null!;
    }
}
