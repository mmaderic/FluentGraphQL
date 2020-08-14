
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLPropertyStatement : IGraphQLStatement
    {
        string PropertyName { get; set; }
        bool IsActive { get; set; }
    }
}
