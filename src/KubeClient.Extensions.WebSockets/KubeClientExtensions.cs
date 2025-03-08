using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.Net.WebSockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient
{
    using Extensions.WebSockets;
    using Http;

    /// <summary>
    ///     WebSockets extension methods for <see cref="KubeClient"/>.
    /// </summary>
    public static class KubeClientExtensions
    {
        /// <summary>
        ///     Apply client web-socket options from the specified Kubernetes API client.
        /// </summary>
        /// <param name="socketOptions">
        ///     The <see cref="ClientWebSocketOptions"/> to configure.
        /// </param>
        /// <param name="client">
        ///     The <see cref="IKubeApiClient"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ApplyClientOptions(this  ClientWebSocketOptions socketOptions, IKubeApiClient client)
        {
            if (socketOptions == null)
                throw new ArgumentNullException(nameof(socketOptions));

            if (client == null)
                throw new ArgumentNullException(nameof(client));

            KubeClientOptions clientOptions = client.GetClientOptions();

            if (!String.IsNullOrWhiteSpace(clientOptions.AccessToken))
                socketOptions.SetRequestHeader("Authorization", $"Bearer {clientOptions.AccessToken}");

            if (clientOptions.ClientCertificate != null)
                socketOptions.ClientCertificates.Add(clientOptions.ClientCertificate);

            if (clientOptions.CertificationAuthorityCertificate != null)
            {
                socketOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    if (sslPolicyErrors != SslPolicyErrors.RemoteCertificateChainErrors)
                        return false;

                    try
                    {
                        using (X509Chain certificateChain = new X509Chain())
                        {
                            certificateChain.ChainPolicy.ExtraStore.Add(clientOptions.CertificationAuthorityCertificate);
                            certificateChain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;
                            certificateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

                            return certificateChain.Build(
                                (X509Certificate2)certificate
                            );
                        }
                    }
                    catch (Exception chainException)
                    {
                        ILoggerFactory loggerFactory = clientOptions.LoggerFactory;
                        if (loggerFactory != null)
                        {
                            loggerFactory.CreateLogger(typeof(KubeClientExtensions))
                                .LogError(chainException, "Failed to build X.509 certificate chain for remote certificate.");
                        }
                        else
                            Debug.WriteLine(chainException);

                        return false;
                    }
                };
            }
            else if (clientOptions.AllowInsecure)
                socketOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        ///     Open a WebSocket connection using the <see cref="K8sChannelProtocol.V1"/> sub-protocol.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="targetUri">
        ///     The target URI.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket(this IKubeApiClient client, string targetUri, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (String.IsNullOrWhiteSpace(targetUri))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'targetUri'.", nameof(targetUri));
            
            return client.ConnectWebSocket(new Uri(client.ApiEndPoint, targetUri), cancellationToken);
        }

        /// <summary>
        ///     Open a WebSocket connection using the <see cref="K8sChannelProtocol.V1"/> sub-protocol.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="targetUri">
        ///     The target URI.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static async Task<WebSocket> ConnectWebSocket(this IKubeApiClient client, Uri targetUri, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (targetUri == null)
                throw new ArgumentNullException(nameof(targetUri));

            if (!targetUri.IsAbsoluteUri)
                targetUri = new Uri(client.ApiEndPoint, targetUri);
            
            UriBuilder targetUriBuilder = new UriBuilder(targetUri);
            switch (targetUriBuilder.Scheme)
            {
                case "ws":
                case "wss":
                {
                    break;
                }
                case "http":
                {
                    targetUriBuilder.Scheme = "ws";

                    break;
                }
                case "https":
                {
                    targetUriBuilder.Scheme = "wss";
                    
                    break;
                }
                default:
                {
                    throw new ArgumentException($"Target URI has invalid scheme '{targetUriBuilder.Scheme}' (expected one of 'http', 'https', 'ws', or 'wss').", nameof(targetUri));
                }
            }

            targetUri = targetUriBuilder.Uri;

            ClientWebSocket webSocket = new ClientWebSocket();

            try
            {
                ClientWebSocketOptions webSocketOptions = webSocket.Options;
                webSocketOptions.ApplyClientOptions(client);
                webSocketOptions.AddSubProtocol(K8sChannelProtocol.V1);
                webSocketOptions.SetBuffer(
                    receiveBufferSize: 2048,
                    sendBufferSize: 2048
                );

                await webSocket.ConnectAsync(targetUri, cancellationToken).ConfigureAwait(false);

                return webSocket;
            }
            catch (Exception)
            {
                // Clean up.
                using (webSocket)
                {
                    throw;
                }
            }
        }

        /// <summary>
        ///     Open a WebSocket connection using the <see cref="K8sChannelProtocol.V1"/> sub-protocol.
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
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket<TParameters>(this IKubeApiClient client, string targetUri, TParameters uriTemplateParameters, CancellationToken cancellationToken = default)
            where TParameters : class
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (uriTemplateParameters == null)
                throw new ArgumentNullException(nameof(uriTemplateParameters));

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

            return client.ConnectWebSocket(new UriTemplate(targetUri), parameters, cancellationToken);
        }

        /// <summary>
        ///     Open a WebSocket connection using the <see cref="K8sChannelProtocol.V1"/> sub-protocol.
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
        /// <param name="cancellationToken">
        ///     An optional cancellation token that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The configured <see cref="WebSocket"/>.
        /// </returns>
        public static Task<WebSocket> ConnectWebSocket(this IKubeApiClient client, UriTemplate targetUri, Dictionary<string, string> templateParameters, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (targetUri == null)
                throw new ArgumentNullException(nameof(targetUri));
            
            if (templateParameters == null)
                throw new ArgumentNullException(nameof(templateParameters));
            
            return client.ConnectWebSocket(
                targetUri.Populate(client.ApiEndPoint, templateParameters),
                cancellationToken
            );
        }

        /// <summary>
        ///     Create a Kubernetes-style multiplexed connection over the WebSocket.
        /// </summary>
        /// <param name="websocket">
        ///     The <see cref="WebSocket"/>.
        /// </param>
        /// <param name="inputStreamIndexes">
        ///     An array of bytes containing the indexes of the expected input streams.
        /// </param>
        /// <param name="outputStreamIndexes">
        ///     An array of bytes containing the indexes of the expected output streams.
        /// </param>
        /// <param name="loggerFactory">
        ///     An optional <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        /// <returns>
        ///     The configured <see cref="K8sMultiplexer"/>.
        /// </returns>
        public static K8sMultiplexer Multiplexed(this WebSocket websocket, byte[] inputStreamIndexes = null, byte[] outputStreamIndexes = null, ILoggerFactory loggerFactory = null)
        {
            if (websocket == null)
                throw new ArgumentNullException(nameof(websocket));
            
            if (!(inputStreamIndexes?.Length > 0 || outputStreamIndexes?.Length > 0))
                throw new ArgumentException($"Must specify at least one of {nameof(inputStreamIndexes)} or {nameof(outputStreamIndexes)}.");

            if (loggerFactory == null)
                loggerFactory = new LoggerFactory();

            K8sMultiplexer multiplexer = null;
            try
            {
                multiplexer = new K8sMultiplexer(websocket, inputStreamIndexes, outputStreamIndexes, loggerFactory);
                multiplexer.Start();

                return multiplexer;
            }
            catch (Exception)
            {
                using (multiplexer)
                    throw;
            }
        }

        /// <summary>
        ///     Create a Kubernetes-style multiplexed connection over the WebSocket.
        /// </summary>
        /// <param name="webSocketTask">
        ///     The <see cref="WebSocket"/>.
        /// </param>
        /// <param name="inputStreamIndexes">
        ///     An array of bytes containing the indexes of the expected input streams.
        /// </param>
        /// <param name="outputStreamIndexes">
        ///     An array of bytes containing the indexes of the expected output streams.
        /// </param>
        /// <param name="loggerFactory">
        ///     An optional <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        /// <returns>
        ///     The configured <see cref="K8sMultiplexer"/>.
        /// </returns>
        public static async Task<K8sMultiplexer> Multiplexed(this Task<WebSocket> webSocketTask, byte[] inputStreamIndexes = null, byte[] outputStreamIndexes = null, ILoggerFactory loggerFactory = null)
        {
            if (webSocketTask == null)
                throw new ArgumentNullException(nameof(webSocketTask));
            
            WebSocket webSocket = await webSocketTask;

            try
            {
                return webSocket.Multiplexed(inputStreamIndexes, outputStreamIndexes, loggerFactory);
            }
            catch (Exception)
            {
                using (webSocket)
                    throw;
            }
        }
    }
}
