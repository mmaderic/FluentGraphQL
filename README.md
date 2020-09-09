# FluentGraphQL
> GraphQL client with query builder for .NET

[![NuGet](https://img.shields.io/nuget/v/FluentGraphQL.Client)](https://www.nuget.org/packages/FluentGraphQL.Client)
[![Nuget](https://img.shields.io/nuget/dt/FluentGraphQL.Client)](https://www.nuget.org/packages/FluentGraphQL.Client)

Please follow this link for more comprehensive information: [Documentation](https://github.com/mmaderic/FluentGraphQL/tree/master/Documentation)

<br />

### Installing FluentGraphQL

[FluentGraphQL should be installed with NuGet](https://www.nuget.org/packages/FluentGraphQL.Client):

    Install-Package FluentGraphQL.Client
    
Or via the .NET Core command line interface:

    dotnet add package FluentGraphQL.Client

Either commands, from Package Manager Console or .NET Core CLI, will download and install FluentGraphQL.Client and all required dependencies.

*If query builder needs to be used without the client, it can be installed as standalone library.*

### Configuring Client

In order to use FluentGraphQL.Client in .NET applications, it is required to register it's dependencies using some of the dependeny injection providers.
This example will demonstrate basic configuration using built in .NET Core dependency injection. 

```
  services.AddGraphQLClient(x =>
  {
      var options = new GraphQLOptions
      {
          AdminHeaderName = "some-header-name",
          AdminHeaderSecret = "my admin secret",
          NamingStrategy = NamingStrategy.SnakeCase,
          UseAdminHeader = true,                    
          HttpClientProvider = () =>
          {
            var httpClientFactory = x.GetRequiredService<IHttpClientFactory>();
            return httpClientFactory.CreateClient("GraphQL");
          }
      };

      return options;
  });

```
This example is explicitly using admin header in all requests by setting 'UseAdminHeader' to true. \
In order to use 'AuthenticationHeaderValue' instead, 'AuthenticationHeaderProvider' option should be used. This settings can be overriden by using admin header explicitly for mutations or queries exclusive. \
Please refer to the [Options](https://github.com/mmaderic/FluentGraphQL/blob/master/Documentation/02.options.md) documentation for more detailed information.

*Admin header can be explicitly set at any point of time by using UseAdminHeader() client method*

In order to use client, IGraphQLClient interface should be injected into depending handlers/services.

```
  public class MyHandler
  {
      private readonly IGraphQLClient _graphQLClient;

      public MyHandler(IGraphQLClient graphQLClient)
      {
          _graphQLClient = graphQLClient;
      }
  }
```

### Using Query builder

Query, mutation and action builders are provided by the client instance. After having defined query parameters for query or mutation builders, either *Build()* or *Select()* methods should be used in order to generate the query. Action builder needs Query or Mutation method having defined instead.

```
 var query = client.QueryBuilder<Book>().Where(x => x.Title.Equals("C++ Demystified")).Build();
 var mutation = client.MutationBuilder<Book>().Insert(
    new Book
    {
      Title = "My new book",
      ReleaseDate = DateTime.Now
    }).Build();
    
 var action = client.ActionBuilder().Query(new MyQueryActionRequest());

```

*Select() method allows fragmentation of results, meaning it is possible to select which properties should be returned from the requested entity. As Select returns anonymous object, it can be casted explicitly to entity type by using Cast() method.*

### Using Client

After having GraphQL method constructs defined, execution is simple:

```
  var response = await client.ExecuteAsync(query);
```

Or establishing subscription upon the defined query:

```
  var subscription = await client.SubscribeAsync(query, (response) =>
  {
      Console.WriteLine(response.Name);
  });
```

In order to dispose subscription DisposeAsync() method should be used:

```
  await subscription.DisposeAsync(); 
```

#### Execute multiple queries/mutations as single transaction

After having defined multiple constructs of the same method, *(Queries cannot be combined with mutations)*, transaction object can be constructed using 'GraphQLTransaction' static factory class:

```
  var transaction = GraphQLTransaction.Construct(mutationA, mutationB, mutationC);
  var response = await client.ExecuteAsync(transaction);
```
*Actions can be used as part of transactions too, as beeing either mutation or query.*

Currently transaction factory supports up to 5 distinct method constructs which will have strongly typed response. Support for larger numbers is planned for some of the upcoming releases together with the unlimited transaction execution without strongly typed response objects.

```
  var firstResult = transactionResponse.First;
  var secondResult = transactionResponse.Second;
```

#### Hasura GraphQL engine action response

Actions have custom response type in order to support faulted responses from the handlers. Please see 'IGraphQLActionResponse' interface for details.



