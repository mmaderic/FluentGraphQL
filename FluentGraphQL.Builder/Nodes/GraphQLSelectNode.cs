/*
    MIT License

    Copyright (c) 2020 Mateo Mađerić

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
*/

using FluentGraphQL.Builder.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentGraphQL.Builder.Nodes
{
    public class GraphQLSelectNode : IGraphQLSelectNode
    {
        public IGraphQLHeaderNode HeaderNode { get; set; }
        public IEnumerable<IGraphQLPropertyStatement> PropertyStatements { get; set; }
        public IEnumerable<IGraphQLSelectNode> ChildSelectNodes { get; set; }
        public IEnumerable<IGraphQLSelectNode> AggregateContainerNodes { get; set; }
        public bool IsCollectionNode { get; set; }
        public Type EntityType { get; set; }
        public bool IsActive { get; set; }

        public GraphQLSelectNode(
            IGraphQLHeaderNode headerNode, IEnumerable<IGraphQLPropertyStatement> propertyStatements,
            IEnumerable<IGraphQLSelectNode> childSelectNodes, IEnumerable<IGraphQLSelectNode> aggregateContainerNodes,
            Type entityType, bool isCollectionNode = false, bool active = true)
        {
            HeaderNode = headerNode;
            PropertyStatements = propertyStatements;
            ChildSelectNodes = childSelectNodes;
            AggregateContainerNodes = aggregateContainerNodes;
            IsCollectionNode = isCollectionNode;
            EntityType = entityType;
            IsActive = active;

            if (IsActive == false)
                DeactivateAll();  
        }

        public void ActivateAll()
        {
            IsActive = true;
            Parallel.ForEach(PropertyStatements, (item) => { item.IsActive = true; });
            Parallel.ForEach(ChildSelectNodes, (item) => { item.ActivateAll(); });
        }

        public void DeactivateAll()
        {
            IsActive = false;
            Parallel.ForEach(PropertyStatements, (item) => { item.IsActive = false; });
            Parallel.ForEach(ChildSelectNodes, (item) => { item.DeactivateAll(); });
        }

        public void ActivateNode<TNode>()
        {
            var node = GetChildNode<TNode>();
            node.ActivateAll();
        }

        public void ActivateProperty(string propertyName)
        {
            PropertyStatements.First(x => x.PropertyName.Equals(propertyName)).IsActive = true;
        }

        public virtual IGraphQLSelectNode GetChildNode<TEntity>()
        {
            if (EntityType.Equals(typeof(TEntity)))
                return this;

            return ChildSelectNodes.Select(x => x.GetChildNode<TEntity>()).FirstOrDefault(x => !(x is null));
        }

        public virtual string ToString(IGraphQLStringFactory graphQLStringFactory)
        {            
            return graphQLStringFactory.Construct(this);
        }

        public override string ToString()
        {
            return HeaderNode.Title.ToString();
        }

        public IGraphQLSelectNode GetChildNode(string name)
        {
            if (HeaderNode.Title.Equals(name))
                return this;

            return ChildSelectNodes.Select(x => x.GetChildNode(name)).FirstOrDefault(x => !(x is null));
        }

        public bool HasAggregateContainer()
        {
            if (AggregateContainerNodes.Any(x => x.IsActive))
                return true;

            return ChildSelectNodes.Any(x => x.HasAggregateContainer());
        }
    }
}
