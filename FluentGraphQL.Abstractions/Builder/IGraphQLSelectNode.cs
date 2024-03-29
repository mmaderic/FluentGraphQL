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

using System;
using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLSelectNode : IGraphQLSelectStatement, IGraphQLStatement
    {
        IGraphQLHeaderNode HeaderNode { get; set; }
        IEnumerable<IGraphQLPropertyStatement> PropertyStatements { get; set; }
        IEnumerable<IGraphQLSelectNode> ChildSelectNodes { get; set; }
        IEnumerable<IGraphQLSelectNode> AggregateContainerNodes { get; set; }
        bool IsCollectionNode { get; set; }
        Type EntityType { get; set; }
        bool IsActive { get; set; }

        IGraphQLSelectNode FindNode<TEntity>();
        IGraphQLSelectNode GetChildNode(string name);
        IGraphQLSelectStatement Get(string statementName);

        void ActivateNode<TNode>();
        void ActivateProperty(string propertyName);
        void SetHierarchyLevel(int parentLevel);
    }
}
