
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLPropertyValue : IGraphQLValue
    {
        string ValueLiteral { get; set; }
    }
}
