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

        IGraphQLActionBuilder ActionBuilder();

        Task<TEntity> ExecuteAsync<TEntity>(IGraphQLSingleQuery<TEntity> graphQLQuery);
        Task<TResult> ExecuteAsync<TEntity, TResult>(IGraphQLSingleSelectedQuery<TEntity, TResult> graphQLQuery);

        Task<List<TEntity>> ExecuteAsync<TEntity>(IGraphQLStandardQuery<TEntity> graphQLQuery);
        Task<List<TResult>> ExecuteAsync<TEntity, TResult>(IGraphQLStandardSelectedQuery<TEntity, TResult> graphQLQuery);

        Task<IGraphQLMultipleQueryResponse<TResponseA, TResponseB>> 
            ExecuteAsync<TResponseA, TResponseB>(IGraphQLMultipleQuery<TResponseA, TResponseB> graphQLMultipleQuery);

        Task<IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC>> 
            ExecuteAsync<TResponseA, TResponseB, TResponseC>(IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC> graphQLMultipleQuery);

        Task<IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC, TResponseD>>
            ExecuteAsync<TResponseA, TResponseB, TResponseC, TResponseD>(IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD> graphQLMultipleQuery);

        Task<IGraphQLMultipleQueryResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>>
            ExecuteAsync<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(IGraphQLMultipleQuery<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> graphQLMultipleQuery);

        Task<TEntity> ExecuteAsync<TEntity>(IGraphQLReturnSingleMutation<TEntity> graphQLReturnSingleMutation);
        Task<TReturn> ExecuteAsync<TEntity, TReturn>(IGraphQLSelectedReturnSingleMutation<TEntity, TReturn> graphQLSelectedReturnSingleMutation);

        Task<IGraphQLMutationReturningResponse<TEntity>> ExecuteAsync<TEntity>(IGraphQLReturnMultipleMutation<TEntity> graphQLReturnMultipleMutation);
        Task<IGraphQLMutationReturningResponse<TReturn>> ExecuteAsync<TEntity, TReturn>(IGraphQLSelectedReturnMultipleMutation<TEntity, TReturn> graphQLSelectedReturnMultipleMutation);

        Task<IGraphQLActionResponse<TResult>> ExecuteAsync<TResult>(IGraphQLQueryAction<TResult> graphQLQueryAction);
        Task<IGraphQLActionResponse<TResult>> ExecuteAsync<TResult>(IGraphQLMutationAction<TResult> graphQLMutationAction);
    }
}
                                                                                