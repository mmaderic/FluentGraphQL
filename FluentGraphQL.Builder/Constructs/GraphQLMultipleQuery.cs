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

using FluentGraphQL.Builder.Abstractions;
using System.Collections;
using System.Collections.Generic;

namespace FluentGraphQL.Builder.Constructs
{
    public abstract class GraphQLMultipleQuery : IGraphQLMultipleQuery
    {
        public static GraphQLMultipleQuery<TResponseA, TResponseB> Construct<TResponseA, TResponseB>(
            IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB>(queryA, queryB);
        }

        public static GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB, IGraphQLQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<TResponseA, TResponseB> Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
            IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB, IGraphQLQuery<TResponseC> queryC, IGraphQLQuery<TResponseD> queryD)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD>(queryA, queryB, queryC, queryD);
        }

        public static GraphQLMultipleQuery<TResponseA, TResponseB> Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
            IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB, IGraphQLQuery<TResponseC> queryC, IGraphQLQuery<TResponseD> queryD,
            IGraphQLQuery<TResponseE> queryE)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(queryA, queryB, queryC, queryD, queryE);
        }

        public abstract IEnumerator<IGraphQLQuery> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string ToString(IGraphQLStringFactory graphQLStringFactory)
        {
            return graphQLStringFactory.Construct(this);
        }
    }

    public class GraphQLMultipleQuery<TResponseA, TResponseB> : GraphQLMultipleQuery, 
        IGraphQLMultipleQuery<TResponseA, TResponseB>
    {
        public IGraphQLQuery<TResponseA> QueryA { get; set; }
        public IGraphQLQuery<TResponseB> QueryB { get; set; }

        public GraphQLMultipleQuery(IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB)
        {
            QueryA = queryA;
            QueryB = queryB;
        }

        public override IEnumerator<IGraphQLQuery> GetEnumerator()
        {
            yield return QueryA;
            yield return QueryB;
        }       
    }

    public class GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC> : GraphQLMultipleQuery<TResponseA, TResponseB>, 
        IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC>
    {
        public IGraphQLQuery<TResponseC> QueryC { get; set; }

        public GraphQLMultipleQuery(IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB, IGraphQLQuery<TResponseC> queryC) 
            : base(queryA, queryB)
        {
            QueryC = queryC;
        }

        public override IEnumerator<IGraphQLQuery> GetEnumerator()
        {
            yield return QueryA;
            yield return QueryB;
            yield return QueryC;
        }
    }

    public class GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD> : GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC>,
        IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        public IGraphQLQuery<TResponseD> QueryD { get; set; }

        public GraphQLMultipleQuery(
            IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB, IGraphQLQuery<TResponseC> queryC, IGraphQLQuery<TResponseD> queryD)
            : base(queryA, queryB, queryC)
        {
            QueryD = queryD;
        }

        public override IEnumerator<IGraphQLQuery> GetEnumerator()
        {
            yield return QueryA;
            yield return QueryB;
            yield return QueryC;
            yield return QueryD;
        }
    }

    public class GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD>,
        IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>
    {     
        public IGraphQLQuery<TResponseE> QueryE { get; set; }

        public GraphQLMultipleQuery(
            IGraphQLQuery<TResponseA> queryA, IGraphQLQuery<TResponseB> queryB, IGraphQLQuery<TResponseC> queryC, IGraphQLQuery<TResponseD> queryD,
            IGraphQLQuery<TResponseE> queryE)
            : base(queryA, queryB, queryC, queryD)
        {
            QueryE = queryE;
        }

        public override IEnumerator<IGraphQLQuery> GetEnumerator()
        {
            yield return QueryA;
            yield return QueryB;
            yield return QueryC;
            yield return QueryD;
            yield return QueryE;
        }
    }
}
