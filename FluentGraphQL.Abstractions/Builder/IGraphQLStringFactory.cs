using FluentGraphQL.Abstractions.Enums;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLStringFactory
    {
        string Construct(IGraphQLQuery graphQLQuery);
        string Construct(IGraphQLHeaderNode graphQLHeaderNode);
        string Construct(IGraphQLSelectNode graphQLSelectNode);
        string Construct(IGraphQLCollectionValue graphQLCollectionValue);
        string Construct(IGraphQLObjectValue graphQLObjectValue);
        string Construct(IGraphQLPropertyValue graphQLPropertyValue);
        string Construct(IGraphQLPropertyStatement graphQLPropertyStatement);
        string Construct(IGraphQLValueStatement graphQLValueStatement);
        string Construct(LogicalOperator logicalOperator);
        string Construct(OrderByDirection orderByDirection);
        string Construct(string @string);
        string Desconstruct(string @string);
    }
}
