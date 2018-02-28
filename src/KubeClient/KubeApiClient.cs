using HTTPlease;
using HTTPlease.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient
{
    using Models;
    using ResourceClients;

    /// <summary>
    ///     Client for the Kubernetes API.
    /// </summary>
    public sealed class KubeApiClient
        : IDisposable
    {
        /// <summary>
        ///     Kubernetes resource clients.
        /// </summary>
        readonly ConcurrentDictionary<Type, KubeResourceClient> _clients = new ConcurrentDictionary<Type, KubeResourceClient>();

        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/>.
        /// </summary>
        /// <param name="httpClient">
        ///     The underlying HTTP client.
        /// </param>
        KubeApiClient(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            Http = httpClient;
        }

        /// <summary>
        ///     Dispose of resources being used by the <see cref="KubeApiClient"/>.
        /// </summary>
        public void Dispose() => Http?.Dispose();

        /// <summary>
        ///     The default Kubernetes namespace.
        /// </summary>
        public string DefaultNamespace { get; set; } = "default";

        /// <summary>
        ///     The underlying HTTP client.
        /// </summary>
        public HttpClient Http { get; }

        /// <summary>
        ///     Get or create a Kubernetes resource client of the specified type.
        /// </summary>
        /// <typeparam name="TClient">
        ///     The type of Kubernetes resource client to get or create.
        /// </typeparam>
        /// <param name="clientFactory">
        ///     A delegate that creates the resource client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public TClient ResourceClient<TClient>(Func<TClient> clientFactory)
            where TClient : KubeResourceClient
        {
            if (clientFactory == null)
                throw new ArgumentNullException(nameof(clientFactory));

            return (TClient)_clients.GetOrAdd(typeof(TClient), clientType =>
            {
                TClient resourceClient = clientFactory();
                if (resourceClient == null)
                    throw new InvalidOperationException($"Factory for Kubernetes resource client of type '{clientType.FullName}' returned null.");

                return (KubeResourceClient)resourceClient;
            });
        }

        /// <summary>
        ///     Get or create a Kubernetes resource client of the specified type.
        /// </summary>
        /// <typeparam name="TClient">
        ///     The type of Kubernetes resource client to get or create.
        /// </typeparam>
        /// <param name="clientFactory">
        ///     A delegate that creates the resource client.
        /// </param>
        /// <returns>
        ///     The resource client.
        /// </returns>
        public TClient ResourceClient<TClient>(Func<KubeApiClient, TClient> clientFactory)
            where TClient : KubeResourceClient
        {
            if (clientFactory == null)
                throw new ArgumentNullException(nameof(clientFactory));

            return (TClient)_clients.GetOrAdd(typeof(TClient), clientType =>
            {
                TClient resourceClient = clientFactory(this);
                if (resourceClient == null)
                    throw new InvalidOperationException($"Factory for Kubernetes resource client of type '{clientType.FullName}' returned null.");

                return (KubeResourceClient)resourceClient;
            });
        }

        /// <summary>
        ///     Create and configure a <see cref="KubeApiClient"/> using the specified options.
        /// </summary>
        /// <param name="options">
        ///     The <see cref="KubeClientOptions"/> used to configure the client.
        /// </param>
        /// <param name="logger">
        ///     An optional <see cref="ILogger"/> used to log requests and responses.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeApiClient"/>.
        /// </returns>
        public static KubeApiClient Create(KubeClientOptions options, ILogger logger = null)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.EnsureValid();
            
            var clientBuilder = new ClientBuilder();

            if (options.CertificationAuthorityCertificate != null)
                clientBuilder = clientBuilder.WithServerCertificate(options.CertificationAuthorityCertificate);

            if (options.ClientCertificate != null)
                clientBuilder = clientBuilder.WithClientCertificate(options.ClientCertificate);

            // TODO: Modify HTTPlease to support switches here for logging headers and / or payloads.
            if (logger != null)
                clientBuilder = clientBuilder.WithLogging(logger);

            HttpClient httpClient = clientBuilder.CreateClient(options.ApiEndPoint);

            if (!String.IsNullOrWhiteSpace(options.AccessToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    scheme: "Bearer",
                    parameter: options.AccessToken
                );
            }

            return new KubeApiClient(httpClient)
            {
                DefaultNamespace = options.KubeNamespace
            };
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> using a bearer token for authentication.
        /// </summary>
        /// <param name="apiEndPoint">
        ///     The base address for the Kubernetes API end-point.
        /// </param>
        /// <param name="accessToken">
        ///     The access token to use for authentication to the API.
        /// </param>
        /// <param name="expectServerCertificate">
        ///     An optional server certificate to expect.
        /// </param>
        /// <param name="logger">
        ///     An optional <see cref="ILogger"/> used to log requests and responses.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeApiClient"/>.
        /// </returns>
        public static KubeApiClient Create(string apiEndPoint, string accessToken, X509Certificate2 expectServerCertificate = null, ILogger logger = null)
        {
            return Create(new KubeClientOptions
            {
                ApiEndPoint = apiEndPoint,
                AccessToken = accessToken,
                CertificationAuthorityCertificate = expectServerCertificate
            }, logger);
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> using an X.509 certificate for client authentication.
        /// </summary>
        /// <param name="apiEndPoint">
        ///     The base address for the Kubernetes API end-point.
        /// </param>
        /// <param name="clientCertificate">
        ///     The X.509 certificate to use for client authentication.
        /// </param>
        /// <param name="expectServerCertificate">
        ///     An optional server certificate to expect.
        /// </param>
        /// <param name="logger">
        ///     An optional <see cref="ILogger"/> used to log requests and responses.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeApiClient"/>.
        /// </returns>
        public static KubeApiClient Create(string apiEndPoint, X509Certificate2 clientCertificate, X509Certificate2 expectServerCertificate = null, ILogger logger = null)
        {
            return Create(new KubeClientOptions
            {
                ApiEndPoint = apiEndPoint,
                ClientCertificate = clientCertificate,
                CertificationAuthorityCertificate = expectServerCertificate
            }, logger);
        }

        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> using pod-level configuration.
        /// </summary>
        /// <param name="logger">
        ///     An optional <see cref="ILogger"/> used to log requests and responses.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeApiClient"/>.
        /// </returns>
        /// <remarks>
        ///     Only works from within a container running in a Kubernetes Pod.
        /// </remarks>
        public static KubeApiClient CreateFromPodServiceAccount(ILogger logger = null)
        {
            string kubeServiceHost = Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST");
            if (String.IsNullOrWhiteSpace(kubeServiceHost))
                throw new InvalidOperationException("KubeApiClient.CreateFromPodServiceAccount can only be called when running in a Kubernetes Pod (KUBERNETES_SERVICE_HOST environment variable is not defined).");

            var baseAddress = $"https://kubernetes/";
            string accessToken = File.ReadAllText("/var/run/secrets/kubernetes.io/serviceaccount/token");
            var kubeCACertificate = new X509Certificate2(
                File.ReadAllBytes("/var/run/secrets/kubernetes.io/serviceaccount/ca.crt")
            );

            return Create(new KubeClientOptions
            {
                ApiEndPoint = baseAddress,
                AccessToken = accessToken,
                CertificationAuthorityCertificate = kubeCACertificate
            }, logger);
        }
    }
}
