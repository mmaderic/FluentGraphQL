using System.Collections;
using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLValueFactory
    {
        IGraphQLValue Construct(object @object);
        IEnumerable<IGraphQLValue> ConstructCollection(IEnumerable enumerable);
        IEnumerable<IGraphQLValueStatement> ConstructObject(object @object);
    }
}
