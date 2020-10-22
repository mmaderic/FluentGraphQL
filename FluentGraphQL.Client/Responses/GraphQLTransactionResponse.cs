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

using FluentGraphQL.Client.Abstractions;
using System.Collections.Generic;

namespace FluentGraphQL.Client.Responses
{
    public class GraphQLTransactionResponse<TResponseA, TResponseB> : IGraphQLTransactionResponse<TResponseA, TResponseB>
    {
        protected List<object> Elements { get; set; }

        public TResponseA First { get; }
        public TResponseB Second { get; }

        public object this[int index] => Elements[index]; 

        public GraphQLTransactionResponse(TResponseA first, TResponseB second)
        {
            First = first;
            Second = second;

            Elements = new List<object>
            {
                First,
                Second
            };
        }
    }

    public class GraphQLTransactionResponse<TResponseA, TResponseB, TResponseC> : GraphQLTransactionResponse<TResponseA, TResponseB>,
        IGraphQLTransactionResponse<TResponseA, TResponseB, TResponseC>
    {
        public TResponseC Third { get; }

        public GraphQLTransactionResponse(TResponseA first, TResponseB second, TResponseC third) 
            : base(first, second)
        {
            Third = third;
            Elements.Add(Third);
        }
    }

    public class GraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD> : GraphQLTransactionResponse<TResponseA, TResponseB, TResponseC>,
        IGraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        public TResponseD Fourth { get; }

        public GraphQLTransactionResponse(TResponseA first, TResponseB second, TResponseC third, TResponseD fourth)
            : base(first, second, third)
        {
            Fourth = fourth;
            Elements.Add(Fourth);
        }
    }

    public class GraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : GraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD>,
        IGraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>
    {
        public TResponseE Fifth { get; }

        public GraphQLTransactionResponse(TResponseA first, TResponseB second, TResponseC third, TResponseD fourth, TResponseE fifth)
            : base(first, second, third, fourth)
        {
            Fifth = fifth;
            Elements.Add(Fifth);
        }
    }
}
