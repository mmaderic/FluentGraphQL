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
        public static IGraphQLMultiConstruct<
            List<TResponseA>,
            List<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB)
        {
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>>
                (queryA, queryB, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            List<TResponseA>,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB)
        {
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB>
                (queryA, queryB, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            List<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>>
                (queryA, queryB, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB>
                (queryA, queryB, GraphQLMethod.Query);
        }

        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>>
                (mutationA, mutationB, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB>
            Construct<TResponseA, TResponseB>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB)
        {
            return new GraphQLMultiConstruct<
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
        public static IGraphQLMultiConstruct<
            List<TResponseA>,
            List<TResponseB>,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            List<TResponseA>,
            List<TResponseB>,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            List<TResponseA>,
            TResponseB,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            List<TResponseB>,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            List<TResponseA>,
            TResponseB,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB,
            List<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLArrayQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            List<TResponseB>,
            TResponseC>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLArrayQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectQuery<TResponseA> queryA,
                IGraphQLObjectQuery<TResponseB> queryB,
                IGraphQLObjectQuery<TResponseC> queryC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC>
                (queryA, queryB, queryC, GraphQLMethod.Query);
        }
        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>, IGraphQLMutationReturningResponse<TResponseB>, IGraphQLMutationReturningResponse<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC>
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC>
                (mutationA, mutationB, mutationC, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB, 
            TResponseC> 
            Construct<TResponseA, TResponseB, TResponseC>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC)
        {
            return new GraphQLMultiConstruct<
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
        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>, 
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB, 
                TResponseC,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD>
                (queryA, queryB, queryC, queryD, GraphQLMethod.Query);
        }

        #endregion
        #region mutation constructs
        /* Mutation method constructs */
        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLArrayMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLArrayMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLArrayMutation<TResponseC> mutationC,
                IGraphQLObjectMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            TResponseB,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD>(
                IGraphQLObjectMutation<TResponseA> mutationA,
                IGraphQLObjectMutation<TResponseB> mutationB,
                IGraphQLObjectMutation<TResponseC> mutationC,
                IGraphQLArrayMutation<TResponseD> mutationD)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>>
                (mutationA, mutationB, mutationC, mutationD, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
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
        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                TResponseE,
                List<TResponseD>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                List<TResponseC>,
                TResponseE,
                TResponseD>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                List<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                List<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                List<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                List<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                List<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD,
                List<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Query);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
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
        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseE,
            IGraphQLMutationReturningResponse<TResponseD>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseE,
                IGraphQLMutationReturningResponse<TResponseD>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseE,
            TResponseD>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseE,
                TResponseD>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
            TResponseC,
            TResponseD,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            IGraphQLMutationReturningResponse<TResponseB>,
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
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseD,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            IGraphQLMutationReturningResponse<TResponseC>,
            TResponseD,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            TResponseC,
            IGraphQLMutationReturningResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
            TResponseB,
            TResponseC,
            TResponseD,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLArrayMutation<TResponseA> queryA,
                IGraphQLObjectMutation<TResponseB> queryB,
                IGraphQLObjectMutation<TResponseC> queryC,
                IGraphQLObjectMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            IGraphQLMutationReturningResponse<TResponseA>,
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
            return new GraphQLMultiConstruct<
                IGraphQLMutationReturningResponse<TResponseA>,
                TResponseB,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>,
            IGraphQLMutationReturningResponse<TResponseE>>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLObjectMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
            TResponseA,
            IGraphQLMutationReturningResponse<TResponseB>,
            IGraphQLMutationReturningResponse<TResponseC>,
            IGraphQLMutationReturningResponse<TResponseD>,
            TResponseE>
            Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
                IGraphQLObjectMutation<TResponseA> queryA,
                IGraphQLArrayMutation<TResponseB> queryB,
                IGraphQLArrayMutation<TResponseC> queryC,
                IGraphQLArrayMutation<TResponseD> queryD,
                IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           IGraphQLMutationReturningResponse<TResponseB>,
           IGraphQLMutationReturningResponse<TResponseC>,
           TResponseD,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           IGraphQLMutationReturningResponse<TResponseB>,
           IGraphQLMutationReturningResponse<TResponseC>,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           IGraphQLMutationReturningResponse<TResponseB>,
           TResponseC,
           IGraphQLMutationReturningResponse<TResponseD>,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           IGraphQLMutationReturningResponse<TResponseB>,
           TResponseC,
           IGraphQLMutationReturningResponse<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           IGraphQLMutationReturningResponse<TResponseB>,
           TResponseC,
           TResponseD,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLArrayMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           IGraphQLMutationReturningResponse<TResponseB>,
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
            return new GraphQLMultiConstruct<
                TResponseA,
                IGraphQLMutationReturningResponse<TResponseB>,
                TResponseC,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           IGraphQLMutationReturningResponse<TResponseC>,
           IGraphQLMutationReturningResponse<TResponseD>,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           IGraphQLMutationReturningResponse<TResponseC>,
           IGraphQLMutationReturningResponse<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           IGraphQLMutationReturningResponse<TResponseC>,
           TResponseD,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           IGraphQLMutationReturningResponse<TResponseC>,
           TResponseD,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLArrayMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                IGraphQLMutationReturningResponse<TResponseC>,
                TResponseD,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           TResponseC,
           IGraphQLMutationReturningResponse<TResponseD>,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           TResponseC,
           IGraphQLMutationReturningResponse<TResponseD>,
           TResponseE>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLArrayMutation<TResponseD> queryD,
               IGraphQLObjectMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                IGraphQLMutationReturningResponse<TResponseD>,
                TResponseE>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
           TResponseA,
           TResponseB,
           TResponseC,
           TResponseD,
           IGraphQLMutationReturningResponse<TResponseE>>
           Construct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(
               IGraphQLObjectMutation<TResponseA> queryA,
               IGraphQLObjectMutation<TResponseB> queryB,
               IGraphQLObjectMutation<TResponseC> queryC,
               IGraphQLObjectMutation<TResponseD> queryD,
               IGraphQLArrayMutation<TResponseE> queryE)
        {
            return new GraphQLMultiConstruct<
                TResponseA,
                TResponseB,
                TResponseC,
                TResponseD,
                IGraphQLMutationReturningResponse<TResponseE>>
                (queryA, queryB, queryC, queryD, queryE, GraphQLMethod.Mutation);
        }

        public static IGraphQLMultiConstruct<
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
            return new GraphQLMultiConstruct<
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
