using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLAggregateClause
    {
        Dictionary<string, object> PropertyValues { get; set; }
    }
}
