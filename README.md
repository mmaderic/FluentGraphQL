# FluentGraphQL
> .NET GraphQL client with query builder

[![NuGet](https://img.shields.io/nuget/v/FluentGraphQL.Client)](https://www.nuget.org/packages/FluentGraphQL.Client)
[![Nuget](https://img.shields.io/nuget/dt/FluentGraphQL.Client)](https://www.nuget.org/packages/FluentGraphQL.Client)

Please follow this link for more comprehensive information: [Documentation](https://github.com/mmaderic/FluentGraphQL/tree/master/Documentation)

<br />

### Installing FluentGraphQL

You should install [FluentGraphQL with NuGet](https://www.nuget.org/packages/FluentGraphQL.Client):

    Install-Package FluentGraphQL.Client
    
Or via the .NET Core command line interface:

    dotnet add package FluentGraphQL.Client

Either commands, from Package Manager Console or .NET Core CLI, will download and install FluentGraphQL.Client and all required dependencies.

*If you would like to use only query builder, you can install and use FluentGraphQL.Builder as standalone library.*

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
This example is explicitly using admin header in all requests by setting 'UseAdminHeader' to true. In order to use 'AuthenticationHeaderValue' instead, use 'AuthenticationHeaderProvider' option. You can also override this settings by using admin header explicitly for mutations or queries exclusive.

*Admin header can be explicitly set at any point of time by using UseAdminHeader() client method*

In order to use client, simply inject IGraphQLClient interface into your handlers/services.

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

Query, mutation and action builders are provided by the client instance. After having defined query parameters for query or mutation builders, use either *Build()* or *Select()* methods to generate query. Action builder just needs Query or Mutation method having defined.

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

*Select() method allows fragmentation of results, meaning you are able to select what properties you would like to have returned from the entity. As Select returns anonymous object, it can be casted explicitly to entity type by using Cast() method.*

### Using Client

After having your GraphQL method constructs defined using builders, execution is simple.

```
  var response = await client.ExecuteAsync(query);
```

Or if you would like to subscribe to defined query

```
  var subscription = await client.SubscribeAsync(query, (response) =>
  {
      Console.WriteLine(response.Name);
  });
```

In order to dispose your subscription use DisposeAsync() method.

```
  await subscription.DisposeAsync(); 
```

#### Execute multiple queries/mutations as single transaction

After having defined multiple constructs of the same method, *(Queries cannot be combined with mutations)*, construct transaction object using 'GraphQLTransaction' static factory class

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



