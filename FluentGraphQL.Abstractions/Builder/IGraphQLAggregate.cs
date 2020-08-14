
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLAggregate
    {
        int Count { get; set; }
        IGraphQLAggregateClause Sum { get; set; }
        IGraphQLAggregateClause Avg { get; set; }
        IGraphQLAggregateClause Max { get; set; }
        IGraphQLAggregateClause Min { get; set; }
    }    
}
