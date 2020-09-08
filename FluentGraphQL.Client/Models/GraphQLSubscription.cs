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
using FluentGraphQL.Client.Abstractions;
using System;
using System.Threading.Tasks;

namespace FluentGraphQL.Client.Models
{
    public class GraphQLSubscription : IGraphQLSubscription
    {
        public string Id { get; set; }
        public SubscriptionState State { get; internal set; }

        private readonly Action<byte[]> _responseHandler;
        private readonly Func<string, Action, Task> _unsubscribeHandler;

        private bool _disposed;
        private bool _disposing;

        public GraphQLSubscription(string id, Action<byte[]> responseHandler, Func<string, Action, Task> unsubscribeHandler)
        {
            Id = id;
            _responseHandler = responseHandler;
            _unsubscribeHandler = unsubscribeHandler;
        }

        internal void Receive(byte[] bytes)
        {
            _responseHandler.Invoke(bytes);
        }

        public async Task UnsubscribeAsync()
        {
            if (State != SubscriptionState.Active && State != SubscriptionState.ActivationQueue)
                throw new InvalidOperationException($"Unable to unsubscribe at the current subscription state: {State}");

            await _unsubscribeHandler.Invoke(Id, null);
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed || _disposing)
                return;

            _disposing = true;
            await _unsubscribeHandler.Invoke(Id, () =>
            {
                State = SubscriptionState.Disposed;
                _disposed = true;
                _disposing = false;
            });           
        }   
    }
}
