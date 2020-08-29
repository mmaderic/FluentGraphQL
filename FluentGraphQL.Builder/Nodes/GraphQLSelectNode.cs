﻿/*
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

            if (!active)
                Deactivate();
            else IsActive = true;
        }

        public GraphQLSelectNode(
            IGraphQLPropertyStatement propertyStatement, Type entityType, bool isCollectionNode = false, bool active = true) :
            this(null, new[] { propertyStatement }, Enumerable.Empty<IGraphQLSelectNode>(), Enumerable.Empty<IGraphQLSelectNode>(), entityType, isCollectionNode, active)
        {  
        }

        public void ActivateNode<TNode>()
        {
            GetChildNode<TNode>().Activate();
        }

        public void ActivateProperty(string propertyName)
        {
            PropertyStatements.First(x => x.PropertyName.Equals(propertyName)).Activate();
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

        public IGraphQLSelectStatement Get(string statementName)
        {
            var property = PropertyStatements.FirstOrDefault(x => x.PropertyName.Equals(statementName));
            if (!(property is null))
                return property;

            return ChildSelectNodes.FirstOrDefault(x => x.HeaderNode.Title.Equals(statementName));
        }

        public bool HasAggregateContainer()
        {
            if (AggregateContainerNodes.Any(x => x.IsActive))
                return true;

            return ChildSelectNodes.Any(x => x.HasAggregateContainer());
        }

        public void Activate()
        {
            IsActive = true;
            Parallel.ForEach(PropertyStatements, (item) => { item.Activate(); });
            Parallel.ForEach(ChildSelectNodes, (item) => { item.Activate(); });
        }

        public void Deactivate()
        {
            IsActive = false;
            Parallel.ForEach(PropertyStatements, (item) => { item.Deactivate(); });
            Parallel.ForEach(ChildSelectNodes, (item) => { item.Deactivate(); });
        }
    }
}
