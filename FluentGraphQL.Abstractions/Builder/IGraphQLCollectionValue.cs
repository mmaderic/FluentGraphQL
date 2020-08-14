using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLCollectionValue : IGraphQLValue
    {
        IEnumerable<IGraphQLValue> CollectionItems { get; set; }
    }
}
