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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLClient
    {
        IGraphQLClient UseAdminHeader();

        IGraphQLRootNodeBuilder<TEntity> QueryBuilder<TEntity>()
            where TEntity : IGraphQLEntity;

        IGraphQLMutationBuilder<TEntity> MutationBuilder<TEntity>()
            where TEntity : IGraphQLEntity;

        IGraphQLFunctionQueryBuilder<TEntity> FunctionQueryBuilder<TEntity>(IGraphQLFunction<TEntity> graphQLFunction)
            where TEntity : IGraphQLEntity;

        IGraphQLActionBuilder ActionBuilder();

        Task<TEntity> ExecuteAsync<TEntity>(IGraphQLObjectQuery<TEntity> graphQLObjectQuery);
        Task<TResult> ExecuteAsync<TEntity, TResult>(IGraphQLSelectedObjectQuery<TEntity, TResult> graphQLSelectedObjectQuery);

        Task<List<TEntity>> ExecuteAsync<TEntity>(IGraphQLArrayQuery<TEntity> graphQLArrayQuery);
        Task<List<TResult>> ExecuteAsync<TEntity, TResult>(IGraphQLSelectedArrayQuery<TEntity, TResult> graphQLSelectedArrayQuery);

        Task<TEntity> ExecuteAsync<TEntity>(IGraphQLObjectMutation<TEntity> graphQLObjectMutation);
        Task<TReturn> ExecuteAsync<TEntity, TReturn>(IGraphQLSelectedObjectMutation<TEntity, TReturn> graphQLSelectedObjectMutation);

        Task<IGraphQLMutationResponse<TEntity>> ExecuteAsync<TEntity>(IGraphQLArrayMutation<TEntity> graphQLArrayMutation);
        Task<IGraphQLMutationResponse<TReturn>> ExecuteAsync<TEntity, TReturn>(IGraphQLSelectedArrayMutation<TEntity, TReturn> graphQLSelectedArrayMutation);

        Task<IGraphQLActionResponse<TResult>> ExecuteAsync<TResult>(IGraphQLQueryExtension<TResult> graphQLQueryExtension);
        Task<IGraphQLActionResponse<TResult>> ExecuteAsync<TResult>(IGraphQLMutationExtension<TResult> graphQLMutationExtension);

        Task<IGraphQLSubscription> SubscribeAsync<TEntity>(IGraphQLObjectQuery<TEntity> graphQLObjectQuery, Action<TEntity> subscriptionHandler, Action<Exception> exceptionHandler = null);
        Task<IGraphQLSubscription> SubscribeAsync<TEntity>(IGraphQLArrayQuery<TEntity> graphQLArrayQuery, Action<List<TEntity>> subscriptionHandler, Action<Exception> exceptionHandler = null);

        Task<IGraphQLTransactionResponse<TResponseA, TResponseB>>
            ExecuteAsync<TResponseA, TResponseB>(IGraphQLTransaction<TResponseA, TResponseB> graphQLtransaction);

        Task<IGraphQLTransactionResponse<TResponseA, TResponseB, TResponseC>>
            ExecuteAsync<TResponseA, TResponseB, TResponseC>(IGraphQLTransaction<TResponseA, TResponseB, TResponseC> graphQLtransaction);

        Task<IGraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD>>
            ExecuteAsync<TResponseA, TResponseB, TResponseC, TResponseD>(IGraphQLTransaction<TResponseA, TResponseB, TResponseC, TResponseD> graphQLtransaction);

        Task<IGraphQLTransactionResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>>
            ExecuteAsync<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(IGraphQLTransaction<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> graphQLtransaction);
    }
}
                                                                                