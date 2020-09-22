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
using FluentGraphQL.Builder.Constants;
using FluentGraphQL.Builder.Extensions;
using FluentGraphQL.Client.Abstractions;
using FluentGraphQL.Client.Exceptions;
using FluentGraphQL.Client.Extensions;
using FluentGraphQL.Client.Models;
using FluentGraphQL.Client.Responses;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static FluentGraphQL.Client.Constants.Constant;

namespace FluentGraphQL.Client
{
    public class GraphQLClient : IGraphQLClient
    {
        private readonly IGraphQLClientOptions _graphQLClientOptions;
        private readonly IGraphQLStringFactory _graphQLStringFactory;
        private readonly IGraphQLBuilderFactory _graphQLBuilderFactory;
        private readonly IGraphQLWebSocketProtocolService _graphQLWebSocketService;
        private readonly IGraphQLSerializerOptionsProvider _graphQLSerializerOptionsProvider;

        private readonly Lazy<HttpClient> _httpClient;

        public GraphQLClient(
           IGraphQLClientOptions graphQLClientOptions, IGraphQLStringFactory graphQLStringFactory, IGraphQLBuilderFactory graphQLBuilderFactory,           
           IGraphQLWebSocketProtocolService graphQLWebSocketService, IGraphQLSerializerOptionsProvider graphQLSerializerOptionsProvider)
        {
            _graphQLStringFactory = graphQLStringFactory;
            _graphQLBuilderFactory = graphQLBuilderFactory;
            _graphQLClientOptions = graphQLClientOptions;
            _graphQLSerializerOptionsProvider = graphQLSerializerOptionsProvider;
            _graphQLWebSocketService = graphQLWebSocketService;

            _httpClient = new Lazy<HttpClient>(() => _graphQLClientOptions.HttpClientProvider.Invoke());
        }

        public IGraphQLClient UseAdminHeader()
        {
            _graphQLClientOptions.UseAdminHeader = true;
            return this;
        }

        public IGraphQLRootNodeBuilder<TEntity> QueryBuilder<TEntity>()
            where TEntity : IGraphQLEntity
        {
            return _graphQLBuilderFactory.QueryBuilder<TEntity>();
        }

        public IGraphQLMutationBuilder<TEntity> MutationBuilder<TEntity>() 
            where TEntity : IGraphQLEntity
        {
            return _graphQLBuilderFactory.MutationBuilder<TEntity>();
        }

        public IGraphQLFunctionQueryBuilder<TEntity> FunctionQueryBuilder<TEntity>(IGraphQLFunction<TEntity> graphQLFunction) 
            where TEntity : IGraphQLEntity
        {
            return _graphQLBuilderFactory.FunctionBuilder(graphQLFunction);
        }

        public IGraphQLActionBuilder ActionBuilder()
        {
            return _graphQLBuilderFactory.ActionBuilder();
        }

        public Task<TEntity> ExecuteAsync<TEntity>(IGraphQLSingleQuery<TEntity> graphQLQuery)
        {
            return ExecuteConstructAsync<TEntity>(graphQLQuery);
        }

        public Task<List<TEntity>> ExecuteAsync<TEntity>(IGraphQLStandardQuery<TEntity> graphQLQuery)
        {
            return ExecuteConstructAsync<List<TEntity>>(graphQLQuery);
        }

        public async Task<TResult> ExecuteAsync<TEntity, TResult>(IGraphQLSingleSelectedQuery<TEntity, TResult> graphQLQuery)
        {
            var result = await ExecuteConstructAsync<TEntity>(graphQLQuery).ConfigureAwait(false);
            if (result == null)
                return default;

            return graphQLQuery.Selector.Invoke(result);
        }

        public async Task<List<TResult>> ExecuteAsync<TEntity, TResult>(IGraphQLStandardSelectedQuery<TEntity, TResult> graphQLQuery)
        {
            var result = await ExecuteConstructAsync<List<TEntity>>(graphQLQuery).ConfigureAwait(false);
            return result.Select(x => graphQLQuery.Selector.Invoke(x)).ToList();
        }

        public Task<TEntity> ExecuteAsync<TEntity>(IGraphQLReturnSingleMutation<TEntity> graphQLReturnSingleMutation)
        {
            return ExecuteConstructAsync<TEntity>(graphQLReturnSingleMutation);
        }

        public Task<IGraphQLMutationReturningResponse<TEntity>> ExecuteAsync<TEntity>(IGraphQLReturnMultipleMutation<TEntity> graphQLReturnMultipleMutation)
        {
            return ExecuteConstructAsync<IGraphQLMutationReturningResponse<TEntity>>(graphQLReturnMultipleMutation);
        }

        public async Task<TReturn> ExecuteAsync<TEntity, TReturn>(IGraphQLSelectedReturnSingleMutation<TEntity, TReturn> graphQLSelectedReturnSingleMutation)
        {
            var result = await ExecuteConstructAsync<TEntity>(graphQLSelectedReturnSingleMutation).ConfigureAwait(false);
            return graphQLSelectedReturnSingleMutation.Selector.Invoke(result);
        }

        public async Task<IGraphQLMutationReturningResponse<TReturn>> ExecuteAsync<TEntity, TReturn>(IGraphQLSelectedReturnMultipleMutation<TEntity, TReturn> graphQLSelectedReturnMultipleMutation)
        {
            var result = await ExecuteConstructAsync<IGraphQLMutationReturningResponse<TEntity>>(graphQLSelectedReturnMultipleMutation).ConfigureAwait(false);
            var response = new GraphQLMutationReturningResponse<TReturn>()
            {
                AffectedRows = result.AffectedRows,
                Returning = new List<TReturn>()
            };

            foreach (var item in result.Returning)
                response.Returning.Add(graphQLSelectedReturnMultipleMutation.Selector.Invoke(item));

            return response;
        }

        public Task<IGraphQLActionResponse<TResult>> ExecuteAsync<TResult>(IGraphQLQueryExtension<TResult> graphQLQueryExtension)
        {
            return ExecuteConstructAsync<IGraphQLActionResponse<TResult>>(graphQLQueryExtension);
        }

        public Task<IGraphQLActionResponse<TResult>> ExecuteAsync<TResult>(IGraphQLMutationExtension<TResult> graphQLMutationExtension)
        {
            return ExecuteConstructAsync<IGraphQLActionResponse<TResult>>(graphQLMutationExtension);
        }

        public async Task<IGraphQLMultiResponse<TResponseA, TResponseB>> ExecuteAsync<TResponseA, TResponseB>(IGraphQLMultiConstruct<TResponseA, TResponseB> graphQLMultiConstruct)
        {
            var dictionary = await ExecuteMultipleQueryAsync(graphQLMultiConstruct).ConfigureAwait(false);

            var valueA = ProcessMultiConstructResponseValue<TResponseA>(graphQLMultiConstruct.ConstructA, dictionary);
            var valueB = ProcessMultiConstructResponseValue<TResponseB>(graphQLMultiConstruct.ConstructB, dictionary);

            return new GraphQLMultiResponse<TResponseA, TResponseB>(valueA, valueB);
        }

        public async Task<IGraphQLMultiResponse<TResponseA, TResponseB, TResponseC>> ExecuteAsync<TResponseA, TResponseB, TResponseC>(IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC> graphQLMultiConstruct)
        {
            var dictionary = await ExecuteMultipleQueryAsync(graphQLMultiConstruct).ConfigureAwait(false);

            var valueA = ProcessMultiConstructResponseValue<TResponseA>(graphQLMultiConstruct.ConstructA, dictionary);
            var valueB = ProcessMultiConstructResponseValue<TResponseB>(graphQLMultiConstruct.ConstructB, dictionary);
            var valueC = ProcessMultiConstructResponseValue<TResponseC>(graphQLMultiConstruct.ConstructC, dictionary);

            return new GraphQLMultiResponse<TResponseA, TResponseB, TResponseC>(valueA, valueB, valueC);
        }

        public async Task<IGraphQLMultiResponse<TResponseA, TResponseB, TResponseC, TResponseD>> ExecuteAsync<TResponseA, TResponseB, TResponseC, TResponseD>(IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD> graphQLMultiConstruct)
        {
            var dictionary = await ExecuteMultipleQueryAsync(graphQLMultiConstruct).ConfigureAwait(false);

            var valueA = ProcessMultiConstructResponseValue<TResponseA>(graphQLMultiConstruct.ConstructA, dictionary);
            var valueB = ProcessMultiConstructResponseValue<TResponseB>(graphQLMultiConstruct.ConstructB, dictionary);
            var valueC = ProcessMultiConstructResponseValue<TResponseC>(graphQLMultiConstruct.ConstructC, dictionary);
            var valueD = ProcessMultiConstructResponseValue<TResponseD>(graphQLMultiConstruct.ConstructD, dictionary);

            return new GraphQLMultiResponse<TResponseA, TResponseB, TResponseC, TResponseD>(valueA, valueB, valueC, valueD);
        }

        public async Task<IGraphQLMultiResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>> ExecuteAsync<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(IGraphQLMultiConstruct<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE> graphQLMultiConstruct)
        {
            var dictionary = await ExecuteMultipleQueryAsync(graphQLMultiConstruct).ConfigureAwait(false);

            var valueA = ProcessMultiConstructResponseValue<TResponseA>(graphQLMultiConstruct.ConstructA, dictionary);
            var valueB = ProcessMultiConstructResponseValue<TResponseB>(graphQLMultiConstruct.ConstructB, dictionary);
            var valueC = ProcessMultiConstructResponseValue<TResponseC>(graphQLMultiConstruct.ConstructC, dictionary);
            var valueD = ProcessMultiConstructResponseValue<TResponseD>(graphQLMultiConstruct.ConstructD, dictionary);
            var valueE = ProcessMultiConstructResponseValue<TResponseE>(graphQLMultiConstruct.ConstructE, dictionary);

            return new GraphQLMultiResponse<TResponseA, TResponseB, TResponseC, TResponseD, TResponseE>(valueA, valueB, valueC, valueD, valueE);
        }        

        public Task<IGraphQLSubscription> SubscribeAsync<TEntity>(
            IGraphQLStandardQuery<TEntity> graphQLStandardQuery, Action<List<TEntity>> subscriptionHandler, Action<Exception> exceptionHandler = null)
        {
            return ExecuteSubscription(graphQLStandardQuery, subscriptionHandler, exceptionHandler);          
        } 

        public Task<IGraphQLSubscription> SubscribeAsync<TEntity>(
            IGraphQLSingleQuery<TEntity> graphQLSingleQuery, Action<TEntity> subscriptionHandler, Action<Exception> exceptionHandler = null)
        {
            return ExecuteSubscription(graphQLSingleQuery, subscriptionHandler, exceptionHandler);
        }

        private async Task<IGraphQLSubscription> ExecuteSubscription<TResponse>(IGraphQLQuery graphQLQuery, Action<TResponse> subscriptionHandler, Action<Exception> exceptionHandler)
        {
            graphQLQuery.Method = GraphQLMethod.Subscription;
            var queryString = graphQLQuery.ToString(_graphQLStringFactory);
            var request = new GraphQLRequest
            {
                Query = queryString
            };

            var subscription = await _graphQLWebSocketService.StartSubscriptionAsync(request, (bytes) => ProcessSubscriptionResponse(graphQLQuery, bytes, subscriptionHandler, exceptionHandler))
                .ConfigureAwait(false);

            return subscription;
        }

        private async Task<TResponse> ExecuteConstructAsync<TResponse>(IGraphQLNodeConstruct graphQLNodeConstruct)
        {
            var queryString = graphQLNodeConstruct.ToString(_graphQLStringFactory);
            var request = new GraphQLRequest
            { 
                Query = queryString 
            };

            var content = await ExecuteRequestAsync(request, graphQLNodeConstruct.Method).ConfigureAwait(false);
            using (var document = JsonDocument.Parse(content))
            {
                var root = document.RootElement;
                return (TResponse)DeserializeJsonElement(graphQLNodeConstruct, root, typeof(TResponse));
            }
        }

        private async Task<ConcurrentDictionary<RuntimeTypeHandle, object>> ExecuteMultipleQueryAsync(IGraphQLMultiConstruct graphQLMultiConstruct)
        {
            var queryString = graphQLMultiConstruct.ToString(_graphQLStringFactory);
            var request = new GraphQLRequest { Query = queryString };
            var resultDictionary = new ConcurrentDictionary<RuntimeTypeHandle, object>();
            var content = await ExecuteRequestAsync(request, GraphQLMethod.Query).ConfigureAwait(false);

            using (var document = JsonDocument.Parse(content))
            {
                var options = _graphQLSerializerOptionsProvider.Provide(graphQLMultiConstruct);
                var errors = ReadErrors(document.RootElement, options);
                if (!(errors is null))
                    throw new GraphQLException(errors, queryString);

                Parallel.ForEach(graphQLMultiConstruct, (construct) =>
                {
                    var responseType = ResolveContentType(construct);
                    var result = DeserializeJsonElement(construct, document.RootElement, responseType);

                    resultDictionary.TryAdd(responseType.TypeHandle, result);
                });
            }

            return resultDictionary;
        }

        private async Task<string> ExecuteRequestAsync(IGraphQLRequest graphQLRequest, GraphQLMethod graphQLMethod)
        {
            var stringContent = new StringContent(graphQLRequest.ToString(), Encoding.UTF8, "application/json");
            var httpRequestMessage = new HttpRequestMessage
            {
                Content = stringContent,
                Method = HttpMethod.Post
            };

            await ConfigureAuthenticationHeader(graphQLMethod, httpRequestMessage).ConfigureAwait(false);

            var response = await _httpClient.Value.SendAsync(httpRequestMessage).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return content;
        }

        private object DeserializeJsonElement(IGraphQLNodeConstruct graphQLNodeConstruct, JsonElement jsonElement, Type responseType)
        {
            var key = graphQLNodeConstruct.KeyString(_graphQLStringFactory);
            var options = _graphQLSerializerOptionsProvider.Provide(graphQLNodeConstruct);

            var successful = jsonElement.TryGetProperty(_graphQLStringFactory.Construct(GraphQLResponseKeys.Data), out JsonElement element);
            if (!successful)
                return HandleErrorResponse(jsonElement, options, responseType);

            var data = ReadJsonStringData(element, key, graphQLNodeConstruct, responseType);
            return JsonSerializer.Deserialize(data, responseType, options);
        }

        private GraphQLError[] ReadErrors(JsonElement root, JsonSerializerOptions options)
        {
            var successful = root.TryGetProperty(_graphQLStringFactory.Construct(GraphQLResponseKeys.Errors), out JsonElement errors);
            if (!successful)
                return null;

            return JsonSerializer.Deserialize<GraphQLError[]>(errors.GetRawText(), options);
        }
 
        private async Task ConfigureAuthenticationHeader(GraphQLMethod method, HttpRequestMessage httpRequestMessage)
        {
            void AddAdminHeader()
            {
                httpRequestMessage.Headers.Add(_graphQLClientOptions.AdminHeaderName, _graphQLClientOptions.AdminHeaderSecret);
            }

            if (_graphQLClientOptions.UseAdminHeader)            
                AddAdminHeader();
            else
            {
                bool condition;
                switch (method)
                {
                    case GraphQLMethod.Query:
                        condition = _graphQLClientOptions.UseAdminHeaderForQueries;
                        break;
                    case GraphQLMethod.Mutation:
                        condition = _graphQLClientOptions.UseAdminHeaderForMutations;
                        break;
                    default:
                        throw new NotImplementedException(method.ToString());
                };

                if (condition)
                    AddAdminHeader();

                else if (!(_graphQLClientOptions.AuthenticationHeaderProvider is null))
                    httpRequestMessage.Headers.Authorization = await _graphQLClientOptions.AuthenticationHeaderProvider.Invoke().ConfigureAwait(false);                
            }
        }     

        private string ReadJsonElement(JsonElement jsonElement, bool isSingleItemExecution)
        {
            if (!(isSingleItemExecution && jsonElement.ValueKind.Equals(JsonValueKind.Array)))
                return jsonElement.GetRawText();
            
            if (jsonElement.GetArrayLength() > 1)
                throw new InvalidOperationException("More than one element was found.");

            if (jsonElement.GetArrayLength() == 0)
                return Constant.GraphQLKeyords.Null;

            var enumerator = jsonElement.EnumerateArray();
            enumerator.MoveNext();
               
            return enumerator.Current.GetRawText();    
        }

        private TResponse ProcessMultiConstructResponseValue<TResponse>(IGraphQLNodeConstruct graphQLNodeConstruct, ConcurrentDictionary<RuntimeTypeHandle, object> dictionary)
        {
            var contentType = ResolveContentType(graphQLNodeConstruct);
            var key = contentType.TypeHandle;
            var value = dictionary[key];

            if (graphQLNodeConstruct is IGraphQLSelectableConstruct selectableA && selectableA.IsSelected == true)
                return ProcessSelectedValue<TResponse>(selectableA, value);

            if (graphQLNodeConstruct is IGraphQLSelectableConstruct selectableB && selectableB.IsSelected == false)
                return ProcessSelectedValueAsNamedCast<TResponse>(selectableB, value);

            return (TResponse)value;           
        }

        private TResponse ProcessSelectedValue<TResponse>(IGraphQLSelectableConstruct selectableConstruct, object value)
        {
            if (value is IList list)
            {
                var resultType = typeof(TResponse).GenericTypeArguments.First();
                var listGenericTypeDefinition = list.GetType().GetGenericTypeDefinition();
                var listResultType = listGenericTypeDefinition.MakeGenericType(resultType);
                var listInstance = (IList)Activator.CreateInstance(listResultType);

                foreach (var item in list)
                    listInstance.Add(selectableConstruct.InvokeSelector(item));

                return (TResponse)listInstance;
            }
            else
                return (TResponse)selectableConstruct.InvokeSelector(value);
        }        

        private TResponse ProcessSelectedValueAsNamedCast<TResponse>(IGraphQLSelectableConstruct selectableConstruct, object value)
        {
            void MapSelectInvokeValues(object instance, IGraphQLSelectableConstruct construct)
            {
                var selectedInstanceInvoke = construct.InvokeSelector(instance);
                MapSelectedObjectValues(selectedInstanceInvoke, instance);
            }

            if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                    MapSelectInvokeValues(item, selectableConstruct);
            }
            else
                MapSelectInvokeValues(value, selectableConstruct);

            return (TResponse)value;
        }

        private void MapSelectedObjectValues(object selectedInstance, object resultInstance)
        {
            if (resultInstance is null)
                return;

            var selectedInstanceProperties = selectedInstance.GetType().GetProperties();
            var resultInstanceProperties = resultInstance.GetType().GetProperties();

            foreach (var property in selectedInstanceProperties)
            {
                var selectedInstancePropertyValue = property.GetValue(selectedInstance);
                var resultInstanceProperty = resultInstanceProperties.FirstOrDefault(x => x.Name.Equals(property.Name));

                if (resultInstanceProperty is null)
                {
                    throw new InvalidOperationException(
                        $"Different property name has been defined. Invalid: '{ property.Name }' doesn't exist on entity '{ resultInstance.GetType().Name }'. " +
                        $"Please use original property name when using AsNamed() selected cast.");
                }

                var propertyType = resultInstanceProperty.PropertyType;
                if (propertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(propertyType))                
                    MapSelectedCollectionValues(property, selectedInstance, resultInstance, selectedInstancePropertyValue, resultInstanceProperty);                
                else
                    resultInstanceProperty.SetValue(resultInstance, selectedInstancePropertyValue);
            }
        }

        private void MapSelectedCollectionValues(PropertyInfo property, object selectedInstance, object resultInstance, object selectedInstancePropertyValue, PropertyInfo resultInstanceProperty)
        {
            var entityType = resultInstanceProperty.PropertyType.GenericTypeArguments.First();
            var arguments = selectedInstancePropertyValue.GetType().GenericTypeArguments;

            if (typeof(IGraphQLEntity).IsAssignableFrom(entityType) && arguments.Count() == 2)
            {
                var selectedItemPropertyValue = property.GetValue(selectedInstance);
                var nestedItemCollection = (IList)resultInstanceProperty.GetValue(resultInstance);
                var selectedItemCollection = (IList)selectedItemPropertyValue.GetType().GetMethod("ToList").Invoke(selectedItemPropertyValue, null);

                // In case of exception, parallel mapping will not be executed
                MapSelectedObjectValues(selectedItemCollection[0], nestedItemCollection[0]);

                Parallel.For(1, nestedItemCollection.Count, (i) =>
                {
                    MapSelectedObjectValues(selectedItemCollection[i], nestedItemCollection[i]);
                });
            }
        }

        private Type ResolveContentType(IGraphQLNodeConstruct graphQLNodeConstruct)
        {
            if (graphQLNodeConstruct.IsSingle)
                return graphQLNodeConstruct.SelectNode.EntityType;

            if (graphQLNodeConstruct.Method.Equals(GraphQLMethod.Query))
                return typeof(List<>).MakeGenericType(graphQLNodeConstruct.SelectNode.EntityType);

            if (graphQLNodeConstruct.Method.Equals(GraphQLMethod.Mutation))
                return typeof(IGraphQLMutationReturningResponse<>).MakeGenericType(graphQLNodeConstruct.SelectNode.EntityType.GenericTypeArguments.First());

            throw new NotImplementedException(GraphQLMethod.Mutation.ToString());
        }        

        private object HandleErrorResponse(JsonElement root, JsonSerializerOptions options, Type responseType)
        {
            if (typeof(IGraphQLActionResponse).IsAssignableFrom(responseType))
            {
                var errors = ReadErrors(root, options);
                var errorMessageObject = new
                {
                    ErrorMessage = string.Join(",", errors.Select(x => x.Message))
                };

                return DeserializeActionErrorResponse(errorMessageObject, options, responseType);
            }
            else
                throw new GraphQLException(ReadErrors(root, options));
        }

        private string ReadJsonStringData(JsonElement element, string key, IGraphQLNodeConstruct graphQLNodeConstruct, Type responseType)
        {
            if (!responseType.IsSimple())
                return ReadJsonElement(element.GetProperty(key), graphQLNodeConstruct.IsSingle);                

            var enumerator = element.GetProperty(key).EnumerateObject();
            enumerator.MoveNext();

            return enumerator.Current.Value.GetRawText();
        }

        private void ProcessSubscriptionResponse<TResponse>(IGraphQLQuery graphQLQuery, byte[] bytes, Action<TResponse> subscriptionHandler, Action<Exception> exceptionHandler)
        {
            var content = Encoding.UTF8.GetString(bytes);
            using (var document = JsonDocument.Parse(content))
            {
                var rootElement = document.RootElement;
                var successful = rootElement.TryGetProperty(_graphQLStringFactory.Construct(GraphQLResponseKeys.Payload), out JsonElement element);
                if (!successful)
                    HandleInvalidSubscriptionState(rootElement, exceptionHandler);

                var response = (TResponse)DeserializeJsonElement(graphQLQuery, element, typeof(TResponse));
                subscriptionHandler.Invoke(response);
            }
        }

        private void HandleInvalidSubscriptionState(JsonElement rootElement, Action<Exception> exceptionHandler)
        {
            rootElement.TryGetProperty(_graphQLStringFactory.Construct(GraphQLResponseKeys.Type), out JsonElement element);
            var exception = new InvalidOperationException($"Invalid subscription state: {element.GetRawText()}");

            if (!(exceptionHandler is null))
                exceptionHandler.Invoke(exception);
            else
                throw exception;
        }

        private object DeserializeActionErrorResponse(object errorMessageObject, JsonSerializerOptions options, Type responseType)
        {
            var errorMessageString = JsonSerializer.Serialize(errorMessageObject, options);
            var defaultErrorActionResponse = (IGraphQLActionResponse)JsonSerializer.Deserialize(errorMessageString, typeof(IGraphQLActionResponse<object>), options);

            var resultObjectType = responseType.GenericTypeArguments.First();
            var errorResponseGenericDefinition = defaultErrorActionResponse.GetType().GetGenericTypeDefinition();

            var errorResponseInstance = (IGraphQLActionResponse)Activator.CreateInstance(errorResponseGenericDefinition.MakeGenericType(resultObjectType));
            errorResponseInstance.ErrorMessage = defaultErrorActionResponse.ErrorMessage;

            return errorResponseInstance;
        }        
    }
}
