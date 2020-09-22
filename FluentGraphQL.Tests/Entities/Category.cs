using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;

namespace FluentGraphQL.Tests.Entities
{
    public class Category : IGraphQLEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = null!;
    }
}
