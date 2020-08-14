using System.Collections.Generic;

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLRequest
    {
        string Query { get; set; }
        string OperationName { get; set; }
        List<object> Variables { get; set; }
    }
}
