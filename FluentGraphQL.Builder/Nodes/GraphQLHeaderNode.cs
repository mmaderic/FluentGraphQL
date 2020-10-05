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
using System.Collections.Generic;
using System.Linq;

namespace FluentGraphQL.Builder.Nodes
{
    internal class GraphQLHeaderNode : IGraphQLHeaderNode
    {
        public string Title { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int HierarchyLevel { get; set; }
        public List<IGraphQLStatement> Statements { get; set; }

        private GraphQLHeaderNode()
        {
            Statements = new List<IGraphQLStatement>();
        }

        private GraphQLHeaderNode(GraphQLHeaderNode copy)
        {
            Title = copy.Title;
            Prefix = copy.Prefix;
            Suffix = copy.Suffix;
            HierarchyLevel = copy.HierarchyLevel;
            Statements = copy.Statements.Select(x => x.DeepCopy()).ToList();
        }

        public GraphQLHeaderNode(string title, int hierarchyLevel = 1) : this()
        {
            Title = title;
            HierarchyLevel = hierarchyLevel;
        }

        public GraphQLHeaderNode(string title, string suffix, int hierarchyLevel = 1) 
            : this(title, hierarchyLevel)
        {
            Suffix = suffix;
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

        public IGraphQLHeaderNode DeepCopy()
        {
            return new GraphQLHeaderNode(this);
        }

        IGraphQLStatement IGraphQLStatement.DeepCopy()
        {
            return new GraphQLHeaderNode(this);
        }
    }
}
