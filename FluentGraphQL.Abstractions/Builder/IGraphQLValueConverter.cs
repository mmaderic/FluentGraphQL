
namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLValueConverter
    {
        string Convert(object @object);
    }
}
