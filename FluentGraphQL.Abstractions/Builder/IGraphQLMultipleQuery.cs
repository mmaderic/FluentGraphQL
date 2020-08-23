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

using System.Collections.Generic;

namespace FluentGraphQL.Builder.Abstractions
{
    public interface IGraphQLMultipleQuery : IGraphQLConstruct, IGraphQLQueryString, IEnumerable<IGraphQLQuery>
    {
    }

    public interface IGraphQLMultipleQuery<TResponseA, TResponseB> : IGraphQLMultipleQuery
    {
        IGraphQLQuery QueryA { get; set; }
        IGraphQLQuery QueryB { get; set; }
    }

    public interface IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC> : IGraphQLMultipleQuery<TResponseA, TResponseB>
    {
        IGraphQLQuery QueryC { get; set; }
    }

    public interface IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD> : IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC>
    {
        IGraphQLQuery QueryD { get; set; }
    }

    public interface IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        IGraphQLQuery QueryE { get; set; }
    }
}
