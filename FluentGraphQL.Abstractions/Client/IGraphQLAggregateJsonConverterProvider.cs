using System.Collections.Generic;

namespace FluentGraphQL.Abstractions.Client
{
    public interface IGraphQLAggregateJsonConverterProvider
    {
        ICollection<IGraphQLJsonConverter> Provide();
    }
}
