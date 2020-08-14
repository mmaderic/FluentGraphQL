
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLStatement
    {
        string ToString(IGraphQLStringFactory graphQLStringFactory);
    }
}
