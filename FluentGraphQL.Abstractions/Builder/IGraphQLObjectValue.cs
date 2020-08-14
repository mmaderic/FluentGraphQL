using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLObjectValue : IGraphQLValue
    {
        IEnumerable<IGraphQLValueStatement> PropertyValues { get; set; }
    }
}
