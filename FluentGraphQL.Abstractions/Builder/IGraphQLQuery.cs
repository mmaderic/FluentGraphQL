
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLQuery : IGraphQLConstruct
    {
        IGraphQLHeaderNode HeaderNode { get; set; }
        IGraphQLSelectNode SelectNode { get; set; }

        IGraphQLSelectNode GetSelectNode<TNode>();
        bool HasAggregateContainer();
    }

    public interface IGraphQLQuery<TEntity> : IGraphQLQuery
    {        
    }

    public interface IGraphQLSingleQuery<TEntity> : IGraphQLQuery
    {
    }
}
