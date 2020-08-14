
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLValueStatement : IGraphQLPropertyStatement
    {
        IGraphQLValue Value { get; set; }
    }
}
