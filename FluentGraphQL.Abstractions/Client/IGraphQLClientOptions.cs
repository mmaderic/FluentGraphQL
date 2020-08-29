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

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FluentGraphQL.Client.Abstractions
{
    public interface IGraphQLClientOptions
    {
        Func<Task<AuthenticationHeaderValue>> AuthenticationHeaderProvider { get; set; }
        Func<HttpClient> HttpClientProvider { get; set; }

        bool UseAdminHeader { get; set; }
        bool UseAdminHeaderForQueries { get; set; }
        bool UseAdminHeaderForMutations { get; set; }

        string AdminHeaderName { get; set; }
        string AdminHeaderSecret { get; set; }
    }
}
