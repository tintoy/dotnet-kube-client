using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KubeClient.Tests.ErrorHandling
{
    /// <summary>
    ///     A mock implementation of <see cref="HttpMessageHandler"/> for use in tests.
    /// </summary>
    public sealed class MockMessageHandler
        : HttpMessageHandler
    {
        /// <summary>
        ///     The delegate that handles incoming requests and returns their responses.
        /// </summary>
        readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handler;

        /// <summary>
        ///     Create a new <see cref="MockMessageHandler"/> that performs the specified action when a request is made.
        /// </summary>
        /// <param name="handler">
        ///     An asynchronous <see cref="Func{T1,T2,T2}"/> delegate that is called when each request is made.
        /// </param>
        public MockMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            _handler = handler;
        }

        /// <summary>
        ///     Create a new <see cref="MockMessageHandler"/> that performs the specified action when a request is made.
        /// </summary>
        /// <param name="handler">
        ///     An asynchronous <see cref="Func{T1,T2}"/> delegate that is called when each request is made.
        /// </param>
        public MockMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            _handler = async (request, cancellationToken) =>
            {
                return await handler(request);
            };
        }

        /// <summary>
        ///     Create a new <see cref="MockMessageHandler"/> that performs the specified action when a request is made.
        /// </summary>
        /// <param name="handler">
        ///     A <see cref="Func{T1,T2}"/> delegate that is called when each request is made.
        /// </param>
        public MockMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            _handler = async (request, cancellationToken) =>
            {
                await Task.Yield();

                HttpResponseMessage response = handler(request);

                return response;
            };
        }

        /// <summary>
        ///     Create a new <see cref="MockMessageHandler"/> that performs the specified action when a request is made.
        /// </summary>
        /// <param name="handler">
        ///     A <see cref="Func{T1}"/> delegate that is called when each request is made.
        /// </param>
        public MockMessageHandler(Func<HttpResponseMessage> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            _handler = async (request, cancellationToken) =>
            {
                await Task.Yield();

                HttpResponseMessage response = handler();

                return response;
            };
        }

        /// <summary>
        ///     Create a new <see cref="MockMessageHandler"/> that performs the specified action when a request is made.
        /// </summary>
        /// <param name="handler">
        ///     An <see cref="Action{T1}"/> delegate that is called when each request is made.
        /// </param>
        /// <remarks>
        ///     Because this handler does not return a response message, it should throw an exception.
        /// </remarks>
        public MockMessageHandler(Action<HttpRequestMessage> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            _handler = async (request, cancellationToken) =>
            {
                await Task.Yield();

                handler(request);

                throw new NotImplementedException($"MockMessageHandler was passed an Action<HttpRequestMessage> delegate; this overload should throw an exception.");
            };
        }

        /// <summary>
        ///     Create a new <see cref="MockMessageHandler"/> that performs the specified action when a request is made.
        /// </summary>
        /// <param name="handler">
        ///     An <see cref="Action"/> delegate that is called when each request is made.
        /// </param>
        /// <remarks>
        ///     Because this handler does not return a response message, it should throw an exception.
        /// </remarks>
        public MockMessageHandler(Action handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));
            
            _handler = async (request, cancellationToken) =>
            {
                await Task.Yield();

                handler();

                throw new NotImplementedException($"MockMessageHandler was passed an Action<HttpRequestMessage> delegate; this overload should throw an exception.");
            };
        }

        /// <summary>
        ///     Handle an HTTP request.
        /// </summary>
        /// <param name="request">
        ///     The outgoing HTTP request message.
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The resulting HTTP response message.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _handler(request, cancellationToken);

            return response;
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> that uses the <see cref="MockMessageHandler"/> to handle HTTP requests.
        /// </summary>
        /// <param name="options">
        ///     Optional <see cref="KubeClientOptions"/> that can be used to configure the client.
        /// </param>
        /// <param name="loggerFactory">
        ///     An optional <see cref="ILoggerFactory"/> that can be used for client-level logging.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeApiClient"/>.
        /// </returns>
        public KubeApiClient CreateClient(KubeClientOptions options = null, ILoggerFactory loggerFactory = null)
        {
            if (options == null)
            {
                options = new KubeClientOptions
                {
                    AllowInsecure = true,
                    ApiEndPoint = new Uri("http://localhost:12345")
                };
            }

            HttpClient client = new HttpClient(this)
            {
                BaseAddress = options.ApiEndPoint
            };

            return KubeApiClient.Create(client, options ?? new KubeClientOptions { LoggerFactory = loggerFactory });
        }
    }
}