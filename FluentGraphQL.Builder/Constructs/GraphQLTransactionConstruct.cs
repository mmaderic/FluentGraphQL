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
    public abstract class GraphQLTransactionConstruct : IGraphQLTransaction
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

        public abstract IGraphQLStatement DeepCopy();
    }

    public class GraphQLTransactionConstruct<TResponseA, TResponseB> : GraphQLTransactionConstruct,
        IGraphQLTransaction<TResponseA, TResponseB>
    {
        public IGraphQLNodeConstruct ConstructA { get; set; }
        public IGraphQLNodeConstruct ConstructB { get; set; }

        private GraphQLTransactionConstruct(GraphQLTransactionConstruct<TResponseA, TResponseB> copy)
        {
            ConstructA = (IGraphQLNodeConstruct) copy.ConstructA.DeepCopy();
            ConstructB = (IGraphQLNodeConstruct) copy.ConstructB.DeepCopy();
        }

        protected GraphQLTransactionConstruct()
        {
        }

        public GraphQLTransactionConstruct(IGraphQLNodeConstruct queryA, IGraphQLNodeConstruct queryB, GraphQLMethod graphQLMethod)
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

        public override IGraphQLStatement DeepCopy()
        {
            return new GraphQLTransactionConstruct<TResponseA, TResponseB>(this);
        }
    }

    public class GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC> : GraphQLTransactionConstruct<TResponseA, TResponseB>,
        IGraphQLTransaction<TResponseA, TResponseB, TResponseC>
    {
        public IGraphQLNodeConstruct ConstructC { get; set; }

        private GraphQLTransactionConstruct(GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC> copy)
        {
            ConstructA = (IGraphQLNodeConstruct) copy.ConstructA.DeepCopy();
            ConstructB = (IGraphQLNodeConstruct) copy.ConstructB.DeepCopy();
            ConstructC = (IGraphQLNodeConstruct) copy.ConstructC.DeepCopy();
        }

        protected GraphQLTransactionConstruct()
        {
        }

        public GraphQLTransactionConstruct(IGraphQLNodeConstruct queryA, IGraphQLNodeConstruct queryB, IGraphQLNodeConstruct queryC, GraphQLMethod graphQLMethod) 
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

        public override IGraphQLStatement DeepCopy()
        {
            return new GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC>(this);
        }
    }

    public class GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD> : GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC>,
        IGraphQLTransaction<TResponseA, TResponseB, TResponseC, TResponseD>
    {
        public IGraphQLNodeConstruct ConstructD { get; set; }

        private GraphQLTransactionConstruct(GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD> copy)
        {
            ConstructA = (IGraphQLNodeConstruct) copy.ConstructA.DeepCopy();
            ConstructB = (IGraphQLNodeConstruct) copy.ConstructB.DeepCopy();
            ConstructC = (IGraphQLNodeConstruct) copy.ConstructC.DeepCopy();
            ConstructD = (IGraphQLNodeConstruct) copy.ConstructD.DeepCopy();
        }

        protected GraphQLTransactionConstruct()
        {
        }

        public GraphQLTransactionConstruct(
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

        public override IGraphQLStatement DeepCopy()
        {
            return new GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD>(this);
        }
    }

    public class GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> : GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD>,
        IGraphQLTransaction<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>
    {     
        public IGraphQLNodeConstruct ConstructE { get; set; }

        private GraphQLTransactionConstruct(GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> copy)
        {
            ConstructA = (IGraphQLNodeConstruct) copy.ConstructA.DeepCopy();
            ConstructB = (IGraphQLNodeConstruct) copy.ConstructB.DeepCopy();
            ConstructC = (IGraphQLNodeConstruct) copy.ConstructC.DeepCopy();
            ConstructD = (IGraphQLNodeConstruct) copy.ConstructD.DeepCopy();
            ConstructE = (IGraphQLNodeConstruct) copy.ConstructE.DeepCopy();
        }

        public GraphQLTransactionConstruct(
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

        public override IGraphQLStatement DeepCopy()
        {
            return new GraphQLTransactionConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(this);
        }
    }
}
