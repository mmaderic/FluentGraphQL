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
using FluentGraphQL.Builder.Atoms;
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Constructs;
using FluentGraphQL.Builder.Extensions;
using FluentGraphQL.Builder.Nodes;
using System.Linq;
using System.Reflection;

namespace FluentGraphQL.Builder.Builders
{
    public class GraphQLActionBuilder : IGraphQLActionBuilder
    {
        private readonly IGraphQLSelectNodeFactory _graphQLSelectNodeFactory;
        private readonly IGraphQLValueFactory _graphQLValueFactory;

        public GraphQLActionBuilder(IGraphQLSelectNodeFactory graphQLSelectNodeFactory, IGraphQLValueFactory graphQLValueFactory)
        {
            _graphQLSelectNodeFactory = graphQLSelectNodeFactory;
            _graphQLValueFactory = graphQLValueFactory;
        }

        private GraphQLMethodConstruct<TResponse> BuildAction<TResponse>(IGraphQLAction<TResponse> graphQLAction, GraphQLMethod graphQLMethod)
        {
            var actionType = graphQLAction.GetType();
            var responseType = typeof(TResponse);
            var isSimpleType = responseType.IsSimple();

            var headerNode = new GraphQLHeaderNode(actionType.Name);
            var selectNode = isSimpleType
                ? new GraphQLSelectNode(new GraphQLPropertyStatement(Constant.GraphQLKeyords.DefaultActionResponseSelect), responseType)
                : _graphQLSelectNodeFactory.Construct(responseType);

            selectNode.HeaderNode = headerNode;

            IGraphQLStatement ConstructStatement(PropertyInfo propertyInfo)
            {
                var value = propertyInfo.GetValue(graphQLAction);
                var graphQLValue = _graphQLValueFactory.Construct(value);

                return new GraphQLValueStatement(propertyInfo.Name, graphQLValue);
            }

            headerNode.Statements = actionType.GetProperties().AsParallel().Select(x => ConstructStatement(x)).ToList();
            return new GraphQLMethodConstruct<TResponse>(graphQLMethod, headerNode, selectNode)
            {
                IsSingleItemExecution = true
            };
        }

        IGraphQLQueryAction<TResponse> IGraphQLActionBuilder.Query<TResponse>(IGraphQLAction<TResponse> graphQLAction)
        {
            return BuildAction(graphQLAction, GraphQLMethod.Query);
        }

        IGraphQLMutationAction<TResponse> IGraphQLActionBuilder.Mutation<TResponse>(IGraphQLAction<TResponse> graphQLAction)
        {
            return BuildAction(graphQLAction, GraphQLMethod.Mutation);
        }
    }
}
