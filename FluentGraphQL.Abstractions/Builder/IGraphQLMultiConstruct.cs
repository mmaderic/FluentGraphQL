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
    public interface IGraphQLMultiConstruct : IGraphQLMethodConstruct, IEnumerable<IGraphQLNodeConstruct>
    {
    }

    public interface IGraphQLMultiConstruct<TResponseA, TResponseB> : IGraphQLMultiConstruct
    {
        IGraphQLNodeConstruct ConstructA { get; set; }
        IGraphQLNodeConstruct ConstructB { get; set; }
    }

    public interface IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC> : IGraphQLMultiConstruct<TResponseA, TResponseB>
    {
        IGraphQLNodeConstruct ConstructC { get; set; }
    }

    public interface IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD> : IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC>
    {
        IGraphQLNodeConstruct ConstructD { get; set; }
    }

    public interface IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        IGraphQLNodeConstruct ConstructE { get; set; }
    }
}
