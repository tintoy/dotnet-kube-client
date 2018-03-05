using HTTPlease;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient
{
    using Extensions.WebSockets;

    /// <summary>
    ///     WebSockets extension methods for <see cref="KubeClient"/>.
    /// </summary>
    public static class KubeClientExtensions
    {
        /// <summary>
        ///     Open a WebSocket connection.
        /// </summary>
        /// <typeparam name="TParameters">
        ///     The type to use use as template parameters.
        /// </typeparam>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="targetUri">
        ///     The target URI or URI template.
        /// </param>
        /// <param name="uriTemplateParameters">
        ///     The <typeparamref name="TParameters"/> whose properties will be used as template parameters.
        /// </param>
        /// <param name="webSocketOptions">
        ///     <see cref="K8sWebSocketOptions"/> used to configure the WebSocket connection.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket<TParameters>(this KubeApiClient client, string targetUri, TParameters uriTemplateParameters, K8sWebSocketOptions webSocketOptions, CancellationToken cancellationToken = default)
            where TParameters : class
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (uriTemplateParameters == null)
                throw new ArgumentNullException(nameof(uriTemplateParameters));

            if (webSocketOptions == null)
                throw new ArgumentNullException(nameof(webSocketOptions));

            var parameters = new Dictionary<string, string>();
            
            // yesyesyes, reflection might be "slow", but it's still blazingly fast compared to making a request over the network.
			foreach (PropertyInfo property in typeof(TParameters).GetTypeInfo().DeclaredProperties)
			{
				// Ignore write-only properties.
				if (!property.CanRead)
					continue;
					
				// Public instance properties only.
				if (!property.GetMethod.IsPublic || property.GetMethod.IsStatic)
					continue;

				parameters.Add(property.Name,
                    property.GetValue(uriTemplateParameters)?.ToString()
                );
			}

            return client.ConnectWebSocket(new UriTemplate(targetUri), parameters, webSocketOptions, cancellationToken);
        }

        /// <summary>
        ///     Open a WebSocket connection.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="targetUri">
        ///     The template used to generate the target URI.
        /// </param>
        /// <param name="templateParameters">
        ///     A <see cref="Dictionary{TKey, TValue}"/> containing the template parameters.
        /// </param>
        /// <param name="webSocketOptions">
        ///     <see cref="K8sWebSocketOptions"/> used to configure the WebSocket connection.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket(this KubeApiClient client, UriTemplate targetUri, Dictionary<string, string> templateParameters, K8sWebSocketOptions webSocketOptions, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (targetUri == null)
                throw new ArgumentNullException(nameof(targetUri));
            
            if (templateParameters == null)
                throw new ArgumentNullException(nameof(templateParameters));
            
            return client.ConnectWebSocket(
                targetUri.Populate(client.ApiEndPoint, templateParameters),
                webSocketOptions,
                cancellationToken
            );
        }

        /// <summary>
        ///     Open a WebSocket connection.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="targetUri">
        ///     The target URI.
        /// </param>
        /// <param name="webSocketOptions">
        ///     <see cref="K8sWebSocketOptions"/> used to configure the WebSocket connection.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket(this KubeApiClient client, string targetUri, K8sWebSocketOptions webSocketOptions, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (String.IsNullOrWhiteSpace(targetUri))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'targetUri'.", nameof(targetUri));

            if (webSocketOptions == null)
                throw new ArgumentNullException(nameof(webSocketOptions));
            
            return client.ConnectWebSocket(new Uri(client.ApiEndPoint, targetUri), webSocketOptions, cancellationToken);
        }

        /// <summary>
        ///     Open a WebSocket connection.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="targetUri">
        ///     The target URI.
        /// </param>
        /// <param name="webSocketOptions">
        ///     <see cref="K8sWebSocketOptions"/> used to configure the WebSocket connection.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket(this KubeApiClient client, Uri targetUri, K8sWebSocketOptions webSocketOptions, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (targetUri == null)
                throw new ArgumentNullException(nameof(targetUri));

            if (!targetUri.IsAbsoluteUri)
                targetUri = new Uri(client.ApiEndPoint, targetUri);

            if (targetUri.Scheme != "ws" && targetUri.Scheme != "wss")
                throw new ArgumentException($"Target URI has invalid scheme '{targetUri.Scheme} (expected 'ws' or 'wss').", nameof(targetUri));

            if (webSocketOptions == null)
                throw new ArgumentNullException(nameof(webSocketOptions));

            return K8sWebSocket.ConnectAsync(targetUri, webSocketOptions, cancellationToken);
        }

        /// <summary>
        ///     Create a Kubernetes-style multiplexed connection over the WebSocket.
        /// </summary>
        /// <param name="websocket">
        ///     The <see cref="WebSocket"/>.
        /// </param>
        /// <param name="inputStreamCount">
        ///     The expected number of input streams.
        /// </param>
        /// <param name="outputStreamCount">
        ///     The expected number of output streams.
        /// </param>
        /// <returns>
        ///     The configured <see cref="K8sMultiplexer"/>.
        /// </returns>
        public static K8sMultiplexer Multiplexed(this WebSocket websocket, byte inputStreamCount = 0, byte outputStreamCount = 0)
        {
            if (websocket == null)
                throw new ArgumentNullException(nameof(websocket));
            
            if (inputStreamCount == 0 && outputStreamCount == 0)
                throw new ArgumentException($"Must specify at least one of {nameof(inputStreamCount)} or {nameof(outputStreamCount)}.");

            K8sMultiplexer multiplexer = null;
            try
            {
                multiplexer = new K8sMultiplexer(websocket, inputStreamCount, outputStreamCount);
                multiplexer.Start();

                return multiplexer;
            }
            catch (Exception)
            {
                using (multiplexer)
                    throw;
            }
        }
    }
}