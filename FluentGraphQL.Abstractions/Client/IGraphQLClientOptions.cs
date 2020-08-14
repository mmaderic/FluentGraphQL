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

        bool UseAdminHeaderAlways { get; set; }
        bool UseAdminHeaderForActions { get; set; }
        bool UseAdminHeaderForQueries { get; set; }
        bool UseAdminHeaderForMutations { get; set; }

        string AdminHeaderName { get; set; }
        string AdminHeaderSecret { get; set; }
    }
}
