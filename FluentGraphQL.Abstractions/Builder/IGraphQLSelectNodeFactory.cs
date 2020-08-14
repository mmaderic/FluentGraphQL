using System;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLSelectNodeFactory
    {
        IGraphQLSelectNode Construct(Type type, int hierarchyLevel, Type parentType = null, bool isCollectionNode = false, string explicitNodeName = null);
    }
}
