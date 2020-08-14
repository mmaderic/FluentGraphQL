using FluentGraphQL.Builder.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLClient
    {
        IGraphQLRootNodeBuilder<TEntity> QueryBuilder<TEntity>();
        Task<TEntity> ExecuteAsync<TEntity>(IGraphQLSingleQuery<TEntity> graphQLQuery);
        Task<List<TEntity>> ExecuteAsync<TEntity>(IGraphQLQuery<TEntity> graphQLQuery);
    }
}
