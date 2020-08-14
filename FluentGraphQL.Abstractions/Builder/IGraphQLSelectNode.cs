using System;
using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLSelectNode : IGraphQLNode
    {
        IGraphQLHeaderNode HeaderNode { get; set; }
        IEnumerable<IGraphQLPropertyStatement> PropertyStatements { get; set; }
        IEnumerable<IGraphQLSelectNode> ChildSelectNodes { get; set; }
        IEnumerable<IGraphQLSelectNode> AggregateContainerNodes { get; set; }
        bool IsCollectionNode { get; set; }
        Type EntityType { get; set; }
        bool IsActive { get; set; }

        IGraphQLSelectNode GetChildNode<TEntity>();
        IGraphQLSelectNode GetChildNode(string name);

        void ActivateAll();
        void DeactivateAll();
        void ActivateNode<TNode>();
        void ActivateProperty(string propertyName);
        bool HasAggregateContainer();
    }
}
