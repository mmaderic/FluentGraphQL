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
using System.Collections;
using System.Collections.Generic;

namespace FluentGraphQL.Builder.Constructs
{
    public abstract class GraphQLMultiConstruct : IGraphQLMultiConstruct
    {
        public string QueryString { get; set; }
        public GraphQLMethod Method { get; set; }

        public abstract IEnumerator<IGraphQLNodeConstruct> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string ToString(IGraphQLStringFactory graphQLStringFactory)
        {
            return graphQLStringFactory.Construct(this);
        }
    }

    public class GraphQLMultiConstruct<TResponseA, TResponseB> : GraphQLMultiConstruct,
        IGraphQLMultiConstruct<TResponseA, TResponseB>
    {
        public IGraphQLNodeConstruct ConstructA { get; set; }
        public IGraphQLNodeConstruct ConstructB { get; set; }


        public GraphQLMultiConstruct(IGraphQLNodeConstruct queryA, IGraphQLNodeConstruct queryB, GraphQLMethod graphQLMethod)
        {
            Method = graphQLMethod;

            ConstructA = queryA;
            ConstructB = queryB;
        }

        public override IEnumerator<IGraphQLNodeConstruct> GetEnumerator()
        {
            yield return ConstructA;
            yield return ConstructB;
        }       
    }

    public class GraphQLMultiConstruct<TResponseA, TResponseB, TResponseC> : GraphQLMultiConstruct<TResponseA, TResponseB>,
        IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC>
    {
        public IGraphQLNodeConstruct ConstructC { get; set; }

        public GraphQLMultiConstruct(IGraphQLNodeConstruct queryA, IGraphQLNodeConstruct queryB, IGraphQLNodeConstruct queryC, GraphQLMethod graphQLMethod) 
            : base(queryA, queryB, graphQLMethod)
        {
            ConstructC = queryC;
        }

        public override IEnumerator<IGraphQLNodeConstruct> GetEnumerator()
        {
            yield return ConstructA;
            yield return ConstructB;
            yield return ConstructC;
        }
    }

    public class GraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD> : GraphQLMultiConstruct<TResponseA, TResponseB, TResponseC>,
        IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        public IGraphQLNodeConstruct ConstructD { get; set; }

        public GraphQLMultiConstruct(
            IGraphQLNodeConstruct queryA, IGraphQLNodeConstruct queryB, IGraphQLNodeConstruct queryC, IGraphQLNodeConstruct queryD, GraphQLMethod graphQLMethod)
            : base(queryA, queryB, queryC, graphQLMethod)
        {
            ConstructD = queryD;
        }

        public override IEnumerator<IGraphQLNodeConstruct> GetEnumerator()
        {
            yield return ConstructA;
            yield return ConstructB;
            yield return ConstructC;
            yield return ConstructD;
        }
    }

    public class GraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : GraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD>,
        IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>
    {     
        public IGraphQLNodeConstruct ConstructE { get; set; }

        public GraphQLMultiConstruct(
            IGraphQLNodeConstruct queryA, IGraphQLNodeConstruct queryB, IGraphQLNodeConstruct queryC, IGraphQLNodeConstruct queryD, IGraphQLNodeConstruct queryE, GraphQLMethod graphQLMethod)
            : base(queryA, queryB, queryC, queryD, graphQLMethod)
        {
            ConstructE = queryE;
        }

        public override IEnumerator<IGraphQLNodeConstruct> GetEnumerator()
        {
            yield return ConstructA;
            yield return ConstructB;
            yield return ConstructC;
            yield return ConstructD;
            yield return ConstructE;
        }
    }
}
