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

using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Builder.Abstractions;

namespace FluentGraphQL.Builder.Constructs
{
    public class GraphQLMutation : IGraphQLMutation
    {
        public IGraphQLHeaderNode HeaderNode { get; set; }
        public IGraphQLSelectNode SelectNode { get; set; }

        public string QueryString { get; set; }

        public GraphQLMethod Method => GraphQLMethod.Mutation;
        public bool IsSingleItemExecution { get; set; }

        internal GraphQLMutation(IGraphQLHeaderNode graphQLHeaderNode, IGraphQLSelectNode graphQLSelectNode)
        {
            HeaderNode = graphQLHeaderNode;
            SelectNode = graphQLSelectNode;
        }

        public string ToString(IGraphQLStringFactory graphQLStringFactory)
        {
            return graphQLStringFactory.Construct(this);
        }

        public string KeyString(IGraphQLStringFactory graphQLStringFactory)
        {
            return HeaderNode.KeyString(graphQLStringFactory);
        }

        public bool HasAggregateContainer()
        {
            return SelectNode.HasAggregateContainer();
        }
    }

    public class GraphQLMutation<TEntity> : GraphQLMutation, IGraphQLReturnSingleMutation<TEntity>, IGraphQLReturnMultipleMutation<TEntity>
    {
        public GraphQLMutation(IGraphQLHeaderNode graphQLHeaderNode, IGraphQLSelectNode graphQLSelectNode) 
            : base(graphQLHeaderNode, graphQLSelectNode)
        {
        }
    }
}
