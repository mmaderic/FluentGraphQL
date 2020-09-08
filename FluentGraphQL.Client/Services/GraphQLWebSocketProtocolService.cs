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
using FluentGraphQL.Client.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static FluentGraphQL.Client.Constants.Constant;

namespace FluentGraphQL.Client.Services
{   
    internal class GraphQLWebSocketProtocolService : IGraphQLWebSocketProtocolService
    {
        private readonly IGraphQLSerializerOptionsProvider _graphQLSerializerOptionsProvider;
        private readonly IGraphQLSubscriptionOptions _graphQLSubscriptionOptions;
        private readonly ILogger _logger;

        private readonly ConcurrentDictionary<string, GraphQLSubscription> _subscriptionStore;
        private readonly List<GraphQLWebSocketTerminateTask> _terminateProtocolTasks;
        private readonly AsyncManualResetEvent _receivedAckResponseEvent;
        private readonly AsyncManualResetEvent _terminateProtocolEvent;
        private readonly AsyncManualResetEvent _acceptRequestsEvent;
        private readonly AsyncManualResetEvent _senderStoppedEvent;
        private readonly AsyncManualResetEvent _listenerStoppedEvent;
        private readonly object _terminationMutex;

        private ClientWebSocket _webSocket;
        private CancellationTokenSource _webSocketCancellationSource;
        private AsyncProducerConsumerQueue<GraphQLWebSocketEnqueueRequest> _sendRequestQueue;

        public GraphQLWebSocketProtocolService(
            IGraphQLSerializerOptionsProvider graphQLSerializerOptionsProvider, IGraphQLSubscriptionOptions graphQLSubscriptionOptions, IServiceProvider serviceProvider)
        {
            _graphQLSerializerOptionsProvider = graphQLSerializerOptionsProvider;
            _graphQLSubscriptionOptions = graphQLSubscriptionOptions;
            _logger = serviceProvider.GetService<ILogger<GraphQLWebSocketProtocolService>>();

            _subscriptionStore = new ConcurrentDictionary<string, GraphQLSubscription>();
            _terminateProtocolTasks = new List<GraphQLWebSocketTerminateTask>();
            _receivedAckResponseEvent = new AsyncManualResetEvent();
            _terminateProtocolEvent = new AsyncManualResetEvent();
            _acceptRequestsEvent = new AsyncManualResetEvent();
            _senderStoppedEvent = new AsyncManualResetEvent(true);
            _listenerStoppedEvent = new AsyncManualResetEvent(true);
            _terminationMutex = new object();

            Task.Run(() => ProtocolLifeCycleTask());
        }

        private async Task ProtocolLifeCycleTask(IEnumerable<GraphQLWebSocketEnqueueRequest> sendRequests = null)
        {
            _sendRequestQueue = new AsyncProducerConsumerQueue<GraphQLWebSocketEnqueueRequest>(sendRequests);
            _acceptRequestsEvent.Set();

            await _sendRequestQueue.OutputAvailableAsync();
            await EstablishProtocolAsync();

            await _terminateProtocolEvent.WaitAsync();

            var terminationTask = _terminateProtocolTasks.Last();
            await TerminateProtocolAsync(terminationTask.CloseStatus);

            if (!(terminationTask.Exception is null))
                _graphQLSubscriptionOptions.ExceptionHandler?.Invoke(terminationTask.Exception);

            _terminateProtocolEvent.Reset();
            _ = ProtocolLifeCycleTask(_sendRequestQueue.GetConsumingEnumerable());
        }

        public async Task<IGraphQLSubscription> StartSubscriptionAsync(IGraphQLRequest graphQLRequest, Action<byte[]> responseHandler, Action callback = null)
        {
            var request = new GraphQLWebSocketRequest
            {
                Id = Guid.NewGuid().ToString("N"),
                Type = GraphQLSubscriptionProtocolKeys.Start,
                Payload = graphQLRequest
            };

            var subscription = new GraphQLSubscription(request.Id, responseHandler, CloseSubscriptionAsync)
            {
                State = SubscriptionState.ActivationQueue
            };

            _subscriptionStore.TryAdd(request.Id, subscription);
            await EnqueueSendRequestAsync(request, () =>
            {
                subscription.State = SubscriptionState.Active;
                callback?.Invoke();
            });

            return subscription;
        }

        public async Task CloseSubscriptionAsync(string id, Action callback = null)
        {
            var isTracked = _subscriptionStore.TryGetValue(id, out GraphQLSubscription subscription);
            if (!isTracked)
                throw new InvalidOperationException($"Subscription with the given id: '{id}', was not found.");

            var request = new GraphQLWebSocketRequest
            {
                Id = id,
                Type = GraphQLSubscriptionProtocolKeys.Stop
            };

            subscription.State = SubscriptionState.DeactivationQueue;
            await EnqueueSendRequestAsync(request, () =>
            {
                subscription.State = SubscriptionState.Deactivated;
                UntrackSubscription(subscription);
                callback?.Invoke();
            });
        }

        private async Task EstablishProtocolAsync()
        { 
            InitializeWebSocket();

            await EstablishWebSocketConnectionAsync();
            if (_terminateProtocolEvent.IsSet)
                return;

            _receivedAckResponseEvent.Reset();
            await InitializeGraphQLDataTransferAsync();
            if (_terminateProtocolEvent.IsSet)
                return;

            StartSenderListenerTasks();
        }

        private async Task TerminateProtocolAsync(WebSocketCloseStatus closeStatus)
        {
            _acceptRequestsEvent.Reset();
            _sendRequestQueue.CompleteAdding();
            StopSenderListenerTasks();

            if (!closeStatus.Equals(WebSocketCloseStatus.Empty))
            {
                var request = new GraphQLWebSocketRequest
                {
                    Type = GraphQLSubscriptionProtocolKeys.Terminate
                };

                var options = _graphQLSerializerOptionsProvider.Provide();
                var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request, options);

                await _webSocket.SendAsync(new ArraySegment<byte>(requestBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            await CloseWebSocketConnectionAsync(closeStatus);
        }       

        private async Task InitializeGraphQLDataTransferAsync()
        {
            var request = new GraphQLWebSocketRequest
            {
                Type = GraphQLSubscriptionProtocolKeys.Init
            };

            var options = _graphQLSerializerOptionsProvider.Provide();
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request, options);

            await _webSocket.SendAsync(new ArraySegment<byte>(requestBytes), WebSocketMessageType.Text, true, CancellationToken.None);

            var timeout = _graphQLSubscriptionOptions.AckResponseSecondsTimeout * 1000;
            var cancellationTokenSource = new CancellationTokenSource(timeout);
            var token = cancellationTokenSource.Token;

            _ = ListenAsync(token);
            try
            {
                await _receivedAckResponseEvent.WaitAsync(token);
                cancellationTokenSource.Cancel();
            }
            catch (OperationCanceledException)
            {
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.ProtocolError, new InvalidOperationException("Unable to establish protocol, failed to receive ack response."));
            }
        }

        private async Task EnqueueSendRequestAsync(GraphQLWebSocketRequest request, Action actionDelegate = null)
        {
            var options = _graphQLSerializerOptionsProvider.Provide();
            var requestBytes = JsonSerializer.SerializeToUtf8Bytes(request, options);
            var sendRequestInfo = new GraphQLWebSocketEnqueueRequest
            {
                Id = request.Id,
                Bytes = new ArraySegment<byte>(requestBytes),
                ActionDelegate = actionDelegate
            };
                   
            await _acceptRequestsEvent.WaitAsync();
            await _sendRequestQueue.EnqueueAsync(sendRequestInfo);
        } 

        private async Task<byte[]> ReceiveResponseAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                var buffer = new ArraySegment<byte>(new byte[8192]);

                while (true)
                {                    
                    var webSocketReceiveResult = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                        break;

                    if (webSocketReceiveResult.MessageType != WebSocketMessageType.Text)
                    {
                        RaiseTerminateProtocolEvent(WebSocketCloseStatus.InvalidMessageType, new InvalidOperationException($"Unsupported web socket message type: {webSocketReceiveResult.MessageType}"));
                        break;
                    }

                    memoryStream.Write(buffer.Array, buffer.Offset, webSocketReceiveResult.Count);
                    if (webSocketReceiveResult.EndOfMessage)
                        break;
                }

                return memoryStream.ToArray();
            }
        }

        private async Task ListenAsync(CancellationToken cancellationToken)
        {
            _listenerStoppedEvent.Reset();
            var options = _graphQLSerializerOptionsProvider.Provide();

            try
            {
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    var bytes = await ReceiveResponseAsync();
                    _ = Task.Run(() => ProcessResponseBytes(bytes, options));
                }
            }  
            catch (Exception exception)
            {
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.Empty, exception);
            } 
            finally
            {
                _listenerStoppedEvent.Set();
            }
        }

        private async Task SendRequestsAsync(CancellationToken cancellationToken)
        {
            _senderStoppedEvent.Reset();

            try
            {
                while (true)
                {
                    var isQueueActive = await _sendRequestQueue.OutputAvailableAsync(cancellationToken);
                    if (!isQueueActive)
                        break;

                    var request = await _sendRequestQueue.DequeueAsync(cancellationToken);
                    await _webSocket.SendAsync(request.Bytes, WebSocketMessageType.Text, true, CancellationToken.None);
                    request.ActionDelegate?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.Empty, exception);
            }
            finally
            {
                _senderStoppedEvent.Set();
            }
        }

        private void ProcessResponseBytes(byte[] bytes, JsonSerializerOptions options)
        {
            if (bytes.Length == 0)
                return;

            var response = JsonSerializer.Deserialize<GraphQLWebSocketResponse>(bytes, options);
            if (response.Type.Equals(GraphQLSubscriptionProtocolKeys.KeepAlive))
                return;

            if (response.Type.Equals(GraphQLSubscriptionProtocolKeys.ConnectionError))
            {
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.NormalClosure, new InvalidOperationException("Subscription connection error."));
                return;
            }

            if (response.Type.Equals(GraphQLSubscriptionProtocolKeys.Ack))
            {
                _receivedAckResponseEvent.Set();
                return;
            }

            if (response.Id is null)
            {
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.InvalidMessageType, new InvalidOperationException($"Invalid response type: {response.Type}"));
                return;
            }

            var isTracked = _subscriptionStore.TryGetValue(response.Id, out GraphQLSubscription subscription);
            if (!isTracked)            
                return;            

            if (response.Type.Equals(GraphQLSubscriptionProtocolKeys.Complete))
            {
                subscription.State = SubscriptionState.Complete;
                UntrackSubscription(subscription);
                return;
            }

            var stream = new GraphQLWebSocketResponseStream
            {
                RequestId = response.Id,
                MessageBytes = bytes
            };

            subscription.Receive(stream.MessageBytes);
        }

        private void InitializeWebSocket()
        {
            _webSocket = new ClientWebSocket();
            _webSocket.Options.AddSubProtocol("graphql-ws");
            _webSocket.Options.SetRequestHeader(_graphQLSubscriptionOptions.AdminHeaderName, _graphQLSubscriptionOptions.AdminHeaderSecret);
        }

        private async Task EstablishWebSocketConnectionAsync()
        {
            await _webSocket.ConnectAsync(new Uri(_graphQLSubscriptionOptions.WebSocketEndpoint), CancellationToken.None);
            if (_webSocket.State != WebSocketState.Open)
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.Empty, new InvalidOperationException($"Invalid websocket state. Unable to connect. {_webSocket.State}"));            
        }

        private async Task CloseWebSocketConnectionAsync(WebSocketCloseStatus status)
        {
            if (!_webSocket.State.Equals(WebSocketState.Closed))
                await _webSocket.CloseAsync(status, null, CancellationToken.None);

            _webSocket = null;           
        }

        private void UntrackSubscription(GraphQLSubscription subscription)
        {
            _subscriptionStore.TryRemove(subscription.Id, out GraphQLSubscription _);
            if (_subscriptionStore.Count == 0)
                RaiseTerminateProtocolEvent(WebSocketCloseStatus.NormalClosure);
        }

        private void StartSenderListenerTasks()
        {
            _senderStoppedEvent.Wait();
            _listenerStoppedEvent.Wait();

            _webSocketCancellationSource = new CancellationTokenSource();

            Task.Run(() => SendRequestsAsync(_webSocketCancellationSource.Token));
            Task.Run(() => ListenAsync(_webSocketCancellationSource.Token));            
        }

        private void StopSenderListenerTasks()
        {   
            _webSocketCancellationSource.Cancel();            
        }

        private void RaiseTerminateProtocolEvent(WebSocketCloseStatus closeStatus, Exception exception = null)
        {
            lock (_terminationMutex)
            {
                if (!(exception is null))
                    _logger?.LogError(exception.Message, exception);

                if (_terminateProtocolEvent.IsSet)
                    return;

                _terminateProtocolTasks.Add(new GraphQLWebSocketTerminateTask
                {
                    CloseStatus = closeStatus,
                    Exception = exception,
                    Time = DateTime.Now
                });

                _terminateProtocolEvent.Set();
            }
        }
    }
}
