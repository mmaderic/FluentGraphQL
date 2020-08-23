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

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLStringFactory
    {
        string Construct(IGraphQLQuery graphQLQuery);
        string Construct(IGraphQLMultipleQuery graphQLMultipleQuery);
        string Construct(IGraphQLMutation graphQLInsertMutation);
        string Construct(IGraphQLHeaderNode graphQLHeaderNode);
        string Construct(IGraphQLSelectNode graphQLSelectNode);
        string Construct(IGraphQLCollectionValue graphQLCollectionValue);
        string Construct(IGraphQLObjectValue graphQLObjectValue);
        string Construct(IGraphQLPropertyValue graphQLPropertyValue);
        string Construct(IGraphQLPropertyStatement graphQLPropertyStatement);
        string Construct(IGraphQLValueStatement graphQLValueStatement);
        string Construct(LogicalOperator logicalOperator);
        string Construct(OrderByDirection orderByDirection);
        string Construct(string @string);
        string Desconstruct(string @string);
    }
}
