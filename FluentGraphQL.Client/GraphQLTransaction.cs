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
using FluentGraphQL.Builder.Constructs;
using FluentGraphQL.Client.Abstractions;
using System.Collections.Generic;

namespace FluentGraphQL.Client
{
    public static class GraphQLTransaction
    {
        #region 2 item permutation
        /* 2 Item permutation factory ------------------------------------------------------------------------------------------------------------------------------------------------------*/
        #region query constructs
        /* Query method constructs */
        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>>
                (queryA, queryB, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB>
                (queryA, queryB, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>>
                (queryA, queryB, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB>
                (queryA, queryB, GraphQLMethod.Query);
        }

        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        #endregion
        #endregion

        #region 3 item permutation
        /* 3 Item permutation factory ------------------------------------------------------------------------------------------------------------------------------------------------------*/
        #region query constructs
        /* Query method constructs */
        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            TResponseC>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }
        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>, IGraphQLMutationResponse<TResponseB>, IGraphQLMutationResponse<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB, 
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        #endregion
        #endregion

        #region 4 item permutation
        /* 4 Item permutation factory ------------------------------------------------------------------------------------------------------------------------------------------------------*/
        #region query constructs
        /* Query method constructs */
        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>,
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>,
            TResponseD> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC,
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>,
            List<TResponseD>> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            List<TResponseC>, 
            List<TResponseD>> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            List<TResponseC>,
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC,
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            List<TResponseC>,
            TResponseD> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC, 
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            TResponseC,
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>, 
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC, 
            TResponseD> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayQuery<TResponseA> queryA, 
                IGraphQLObjectQuery<TResponseB> queryB, 
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            TResponseC,
            TResponseD> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB, 
                IGraphQLObjectQuery<TResponseC> queryC, 
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA, 
            TResponseB, 
            List<TResponseC>, 
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB, 
            TResponseC, 
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB, 
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA, 
            TResponseB,
            TResponseC,
            TResponseD> 
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            TResponseB,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }
        #endregion
        #endregion

        #region 5 item permutation
        /* 5 Item permutation factory ------------------------------------------------------------------------------------------------------------------------------------------------------*/
        #region query constructs
        /* Query method constructs */
        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>,
            List<TResponseD>,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>,
            List<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>,
            TResponseE,
            List<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                TResponseE,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>,
            TResponseE,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                TResponseE,
                TResponseD>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC,
            List<TResponseD>,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC,
            List<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC,
            TResponseD,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>,
            List<TResponseD>,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>,
            List<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>,
            TResponseD,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC,
            List<TResponseD>,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC,
            List<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            List<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC,
                IGraphQLObjectQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            List<TResponseC>,
            List<TResponseD>,
            List<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
            TResponseA,
            List<TResponseB>,
            List<TResponseC>,
            List<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC,
                IGraphQLArrayQuery<TResponseD> queryD,
                IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           List<TResponseB>,
           List<TResponseC>,
           TResponseD,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLArrayQuery<TResponseB> queryB,
               IGraphQLArrayQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           List<TResponseB>,
           List<TResponseC>,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLArrayQuery<TResponseB> queryB,
               IGraphQLArrayQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           List<TResponseB>,
           TResponseC,
           List<TResponseD>,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLArrayQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLArrayQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           List<TResponseB>,
           TResponseC,
           List<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLArrayQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLArrayQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           List<TResponseB>,
           TResponseC,
           TResponseD,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLArrayQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           List<TResponseB>,
           TResponseC,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLArrayQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           List<TResponseC>,
           List<TResponseD>,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLArrayQuery<TResponseC> queryC,
               IGraphQLArrayQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           List<TResponseC>,
           List<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLArrayQuery<TResponseC> queryC,
               IGraphQLArrayQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           List<TResponseC>,
           TResponseD,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLArrayQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           List<TResponseC>,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLArrayQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           List<TResponseD>,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLArrayQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           List<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLArrayQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           TResponseD,
           List<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLArrayQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectQuery<TResponseA> queryA,
               IGraphQLObjectQuery<TResponseB> queryB,
               IGraphQLObjectQuery<TResponseC> queryC,
               IGraphQLObjectQuery<TResponseD> queryD,
               IGraphQLObjectQuery<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            TResponseE,
            IGraphQLMutationResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                TResponseE,
                IGraphQLMutationResponse<TResponseD>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            TResponseE,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                TResponseE,
                TResponseD>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            TResponseD,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            IGraphQLMutationResponse<TResponseB>,
            TResponseC,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            TResponseD,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationResponse<TResponseC>,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC,
            IGraphQLMutationResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            IGraphQLMutationResponse<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                IGraphQLMutationResponse<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>,
            IGraphQLMutationResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLObjectMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
            TResponseA,
            IGraphQLMutationResponse<TResponseB>,
            IGraphQLMutationResponse<TResponseC>,
            IGraphQLMutationResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLObjectMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           IGraphQLMutationResponse<TResponseB>,
           IGraphQLMutationResponse<TResponseC>,
           TResponseD,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           IGraphQLMutationResponse<TResponseB>,
           IGraphQLMutationResponse<TResponseC>,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           IGraphQLMutationResponse<TResponseB>,
           TResponseC,
           IGraphQLMutationResponse<TResponseD>,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           IGraphQLMutationResponse<TResponseB>,
           TResponseC,
           IGraphQLMutationResponse<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           IGraphQLMutationResponse<TResponseB>,
           TResponseC,
           TResponseD,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           IGraphQLMutationResponse<TResponseB>,
           TResponseC,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                IGraphQLMutationResponse<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           IGraphQLMutationResponse<TResponseC>,
           IGraphQLMutationResponse<TResponseD>,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           IGraphQLMutationResponse<TResponseC>,
           IGraphQLMutationResponse<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           IGraphQLMutationResponse<TResponseC>,
           TResponseD,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           IGraphQLMutationResponse<TResponseC>,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationResponse<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           IGraphQLMutationResponse<TResponseD>,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           IGraphQLMutationResponse<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                IGraphQLMutationResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           TResponseD,
           IGraphQLMutationResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD,
                IGraphQLMutationResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLTransaction<
           TResponseA,
           TResponseB,
           TResponseC,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLTransactionConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }
        #endregion
        #endregion
    }
}
