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
using System.Collections.Generic;

namespace FluentGraphQL.Builder.Nodes
{
    public class GraphQLHeaderNode : IGraphQLHeaderNode
    {
        public string Title { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int HierarchyLevel { get; set; }
        public List<IGraphQLStatement> Statements { get; set; }

        public GraphQLHeaderNode(string title, int hierarchyLevel = 1)
        {
            Title = title;
            HierarchyLevel = hierarchyLevel;
            Statements = new List<IGraphQLStatement>();
        }

        public override string ToString()
        {
            return Title;
        }

        public string ToString(IGraphQLStringFactory graphQLStringFactory)
        {            
            return graphQLStringFactory.Construct(this);
        }

        public string KeyString(IGraphQLStringFactory graphQLStringFactory)
        {
            return graphQLStringFactory.Construct($"{Prefix}{Title}{Suffix}");
        }
    }
}
