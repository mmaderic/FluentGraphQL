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

using FluentGraphQL.Abstractions.Enums;
using FluentGraphQL.Builder.Abstractions;
using System;

namespace FluentGraphQL.Builder.Constructs
{
    public abstract class GraphQLMethodConstruct : IGraphQLQuery, IGraphQLMutation
    {
        public bool IsSingleItemExecution { get; set; }

        public IGraphQLHeaderNode HeaderNode { get; set; }
        public IGraphQLSelectNode SelectNode { get; set; }

        public GraphQLMethod Method { get; }

        public string QueryString { get; set; }

        protected GraphQLMethodConstruct(GraphQLMethod graphQLMethod, IGraphQLHeaderNode graphQLHeaderNode, IGraphQLSelectNode graphQLSelectNode) 
        {
            Method = graphQLMethod;
            HeaderNode = graphQLHeaderNode;
            SelectNode = graphQLSelectNode;
        }

        public IGraphQLSelectNode GetSelectNode<TEntity>()
        {
            return SelectNode.EntityType.Equals(typeof(TEntity)) ? SelectNode : SelectNode.GetChildNode<TEntity>();
        }

        public bool HasAggregateContainer()
        {
            return SelectNode.HasAggregateContainer();
        }        

        public string KeyString(IGraphQLStringFactory graphQLStringFactory)
        {
            return HeaderNode.KeyString(graphQLStringFactory);
        }

        public string ToString(IGraphQLStringFactory graphQLStringFactory)
        {
            switch (Method)
            {
                case GraphQLMethod.Query:
                    return graphQLStringFactory.Construct((IGraphQLQuery)this);
                case GraphQLMethod.Mutation:
                    return graphQLStringFactory.Construct((IGraphQLMutation)this);
                default:
                    throw new NotImplementedException(Method.ToString());
            }
        }
    }

    public class GraphQLMethodConstruct<TEntity> : GraphQLMethodConstruct, 
        IGraphQLStandardQuery<TEntity>, IGraphQLSingleQuery<TEntity>,
        IGraphQLReturnSingleMutation<TEntity>, IGraphQLReturnMultipleMutation<TEntity>,
        IGraphQLQueryAction<TEntity>, IGraphQLMutationAction<TEntity>
    {
        public GraphQLMethodConstruct(GraphQLMethod graphQLMethod, IGraphQLHeaderNode graphQLHeaderNode, IGraphQLSelectNode graphQLSelectNode) 
            : base(graphQLMethod, graphQLHeaderNode, graphQLSelectNode) 
        { }        
    }    
}