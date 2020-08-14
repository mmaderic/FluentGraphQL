using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLHeaderNode : IGraphQLNode
    {
        string Title { get; set; }
        string Suffix { get; set; }
        int HierarchyLevel { get; set; }
        List<IGraphQLStatement> Statements { get; set; }

        string KeyString(IGraphQLStringFactory graphQLStringFactory);
    }
}
