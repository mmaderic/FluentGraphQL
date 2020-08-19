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

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLQuery : IGraphQLConstruct
    {
        bool IsSingleQuery { get; set; }
        IGraphQLHeaderNode HeaderNode { get; set; }
        IGraphQLSelectNode SelectNode { get; set; }

        IGraphQLSelectNode GetSelectNode<TNode>();
        IGraphQLSelectStatement Get(string statementName);

        bool HasAggregateContainer();
    }

    public interface IGraphQLQuery<TEntity> : IGraphQLQuery
    {
    }

    public interface IGraphQLStandardQuery<TEntity> : IGraphQLQuery<TEntity>
    {        
    }

    public interface IGraphQLSingleQuery<TEntity> : IGraphQLQuery<TEntity>
    {
    }
}
