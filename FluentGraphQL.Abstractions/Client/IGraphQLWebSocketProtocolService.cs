﻿/*
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

using System;
using System.Threading.Tasks;

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLWebSocketProtocolService
    {
        Task<IGraphQLSubscription> StartSubscriptionAsync(IGraphQLRequest graphQLRequest, Action<byte[]> responseHandler, Action callback = null);
        Task CloseSubscriptionAsync(string id, Action callback = null);
    }
}