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

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLMultipleQueryResponse
    {
    }

    public interface IGraphQLMultipleQueryResponse<TResponseA, TResponseB> : IGraphQLMultipleQueryResponse
    {
        TResponseA First { get; }
        TResponseB Second { get; }
    }

    public interface IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC> : IGraphQLMultipleQueryResponse<TResponseA, TResponseB>
    {
        TResponseC Third { get; }
    }

    public interface IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC, TResponseD> : IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC>
    {
        TResponseD Fourth { get; }
    }

    public interface IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        TResponseE Fifth { get; }
    }
}