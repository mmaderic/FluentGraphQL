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
        public static GraphQLMultipleQuery<List<TResponseA>, List<TResponseB>> Construct<TResponseA, TResponseB>(
            IGraphQLStandardQuery<TResponseA> queryA, IGraphQLStandardQuery<TResponseB> queryB)
        {
            return new GraphQLMultipleQuery<List<TResponseA>, List<TResponseB>>(queryA, queryB);
        }

        public static GraphQLMultipleQuery<List<TResponseA>, TResponseB> Construct<TResponseA, TResponseB>(
            IGraphQLStandardQuery<TResponseA> queryA, IGraphQLSingleQuery<TResponseB> queryB)
        {
            return new GraphQLMultipleQuery<List<TResponseA>,TResponseB>(queryA, queryB);
        }

        public static GraphQLMultipleQuery<TResponseA, List<TResponseB>> Construct<TResponseA, TResponseB>(
            IGraphQLSingleQuery<TResponseA> queryA, IGraphQLStandardQuery<TResponseB> queryB)
        {
            return new GraphQLMultipleQuery<TResponseA, List<TResponseB>>(queryA, queryB);
        }

        public static GraphQLMultipleQuery<TResponseA, TResponseB> Construct<TResponseA, TResponseB>(
            IGraphQLSingleQuery<TResponseA> queryA, IGraphQLSingleQuery<TResponseB> queryB)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB>(queryA, queryB);
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public static GraphQLMultipleQuery<List<TResponseA>, List<TResponseB>, List<TResponseC>> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLStandardQuery<TResponseA> queryA, IGraphQLStandardQuery<TResponseB> queryB, IGraphQLStandardQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<List<TResponseA>, List<TResponseB>, List<TResponseC>>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<List<TResponseA>, List<TResponseB>, TResponseC> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLStandardQuery<TResponseA> queryA, IGraphQLStandardQuery<TResponseB> queryB, IGraphQLSingleQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<List<TResponseA>, List<TResponseB>, TResponseC>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<List<TResponseA>, TResponseB, List<TResponseC>> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLStandardQuery<TResponseA> queryA, IGraphQLSingleQuery<TResponseB> queryB, IGraphQLStandardQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<List<TResponseA>, TResponseB, List<TResponseC>>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<TResponseA, List<TResponseB>, List<TResponseC>> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLSingleQuery<TResponseA> queryA, IGraphQLStandardQuery<TResponseB> queryB, IGraphQLStandardQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<TResponseA, List<TResponseB>, List<TResponseC>>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<List<TResponseA>, TResponseB, TResponseC> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLStandardQuery<TResponseA> queryA, IGraphQLSingleQuery<TResponseB> queryB, IGraphQLSingleQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<List<TResponseA>, TResponseB, TResponseC>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<TResponseA, TResponseB, List<TResponseC>> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLSingleQuery<TResponseA> queryA, IGraphQLSingleQuery<TResponseB> queryB, IGraphQLStandardQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB, List<TResponseC>>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<TResponseA, List<TResponseB>, TResponseC> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLSingleQuery<TResponseA> queryA, IGraphQLStandardQuery<TResponseB> queryB, IGraphQLSingleQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<TResponseA, List<TResponseB>, TResponseC>(queryA, queryB, queryC);
        }

        public static GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC> Construct<TResponseA, TResponseB, TResponseC>(
            IGraphQLSingleQuery<TResponseA> queryA, IGraphQLSingleQuery<TResponseB> queryB, IGraphQLSingleQuery<TResponseC> queryC)
        {
            return new GraphQLMultipleQuery<TResponseA, TResponseB, TResponseC>(queryA, queryB, queryC);
        }

        /*-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/        

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
        public IGraphQLQuery QueryA { get; set; }
        public IGraphQLQuery QueryB { get; set; }

        public GraphQLMultipleQuery(IGraphQLQuery queryA, IGraphQLQuery queryB)
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
        public IGraphQLQuery QueryC { get; set; }

        public GraphQLMultipleQuery(IGraphQLQuery queryA, IGraphQLQuery queryB, IGraphQLQuery queryC) 
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
        public IGraphQLQuery QueryD { get; set; }

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
        public IGraphQLQuery QueryE { get; set; }

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
