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
using FluentGraphQL.Client.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FluentGraphQL.Client.Options
{
    public class GraphQLOptions : IGraphQLClientOptions, IGraphQLStringFactoryOptions, IGraphQLSubscriptionOptions
    {
        public Func<Task<AuthenticationHeaderValue>> AuthenticationHeaderProvider { get; set; }
        public Func<IServiceProvider, HttpClient> HttpClientProvider { get; set; }

        public bool UseAdminHeader { get; set; }
        public bool UseAdminHeaderForQueries { get; set; }
        public bool UseAdminHeaderForMutations { get; set; }

        public string AdminHeaderName { get; set; }
        public string AdminHeaderSecret { get; set; }

        public NamingStrategy NamingStrategy { get; set; }

        public string WebSocketEndpoint { get; set; }
        public int AckResponseSecondsTimeout { get; set; } = 15;
        public Action<Exception> ExceptionHandler { get; set; }
    }
}
