
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLEntity
    {
    }

    public interface IGraphQLEntity<TPrimaryKey> : IGraphQLEntity
    {
        TPrimaryKey Id { get; set; }
    }
}
