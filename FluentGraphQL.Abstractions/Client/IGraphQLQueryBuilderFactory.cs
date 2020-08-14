using FluentGraphQL.Builder.Abstractions;

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLQueryBuilderFactory
    {
        IGraphQLRootNodeBuilder<TEntity> Construct<TEntity>();
    }
}
