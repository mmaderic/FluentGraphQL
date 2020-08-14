using FluentGraphQL.Abstractions.Enums;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLStringFactoryOptions
    {
        NamingStrategy NamingStrategy { get; set; }
    }
}
