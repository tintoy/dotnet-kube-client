using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient
{
    /// <summary>
    ///     Options for the Kubernetes API client.
    /// </summary>
    public class KubeClientOptions
    {
        /// <summary>
        ///     Create new <see cref="KubeClientOptions"/>.
        /// </summary>
        public KubeClientOptions()
        {
        }

        /// <summary>
        ///     Create new <see cref="KubeClientOptions"/>.
        /// </summary>
        /// <param name="apiEndPoint">
        ///     The base address of the Kubernetes API end-point.
        /// </param>
        public KubeClientOptions(string apiEndPoint)
        {
            if (String.IsNullOrWhiteSpace(apiEndPoint))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'apiEndPoint'.", nameof(apiEndPoint));

            ApiEndPoint = new Uri(apiEndPoint);
        }

        /// <summary>
        ///     The default Kubernetes namespace to use when no specific namespace is specified.
        /// </summary>
        public string KubeNamespace { get; set; } = "default";
        
        /// <summary>
        ///     The base address of the Kubernetes API end-point.
        /// </summary>
        public Uri ApiEndPoint { get; set; }

        /// <summary>
        ///     The access token used to authenticate to the Kubernetes API.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     The command used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public string AccessTokenCommand { get; set; }

        /// <summary>
        ///     The command arguments used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public string AccessTokenCommandArguments { get; set; }

        /// <summary>
        ///     The Go-style selector used to retrieve the access token from the command output.
        /// </summary>
        public string AccessTokenSelector { get; set; }

        /// <summary>
        ///     The Go-style selector used to retrieve the access token's expiry date/time from the command output.
        /// </summary>
        public string AccessTokenExpirySelector { get; set; }
        
        /// <summary>
        ///     The initial access token used to authenticate to the Kubernetes API.
        /// </summary>
        public string InitialAccessToken { get; set; }
        
        /// <summary>
        ///     The initial token expiry used to authenticate to the Kubernetes API.
        /// </summary>
        public DateTime? InitialTokenExpiryUtc { get; set; }
        
        /// <summary>
        ///     The strategy used for authenticating to the Kubernetes API.
        /// </summary>
        public KubeAuthStrategy AuthStrategy { get; set; }

        /// <summary>
        ///     The client certificate used to authenticate to the Kubernetes API.
        /// </summary>
        public X509Certificate2 ClientCertificate { get; set; }

        /// <summary>
        ///     The expected CA certificate used by the Kubernetes API.
        /// </summary>
        public X509Certificate2 CertificationAuthorityCertificate { get; set; }

        /// <summary>
        ///     Skip verification of the server's SSL certificate?
        /// </summary>
        public bool AllowInsecure { get; set; }

        /// <summary>
        ///     Log request / response headers?
        /// </summary>
        public bool LogHeaders { get; set; }

        /// <summary>
        ///     Load request / response payloads (bodies)?
        /// </summary>
        public bool LogPayloads { get; set; }

        /// <summary>
        ///     Additional assemblies (if any) that contain model types used by the client.
        /// </summary>
        public List<Assembly> ModelTypeAssemblies { get; } = new List<Assembly>();

        /// <summary>
        /// An optional <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </summary>
        public ILoggerFactory LoggerFactory { get; set; }

        /// <summary>
        /// Create a copy of the <see cref="KubeClientOptions"/>.
        /// </summary>
        /// <returns>The new <see cref="KubeClientOptions"/>.</returns>
        public KubeClientOptions Clone()
        {
            var clonedOptions = new KubeClientOptions
            {
                AccessToken = AccessToken,
                AccessTokenCommand = AccessTokenCommand,
                AccessTokenCommandArguments = AccessTokenCommandArguments,
                AccessTokenExpirySelector = AccessTokenExpirySelector,
                AccessTokenSelector = AccessTokenSelector,
                AllowInsecure = AllowInsecure,
                ApiEndPoint = ApiEndPoint,
                AuthStrategy = AuthStrategy,
                CertificationAuthorityCertificate = CertificationAuthorityCertificate,
                ClientCertificate = ClientCertificate,
                InitialAccessToken = InitialAccessToken,
                InitialTokenExpiryUtc = InitialTokenExpiryUtc,
                KubeNamespace = KubeNamespace,
                LoggerFactory = LoggerFactory,
                LogHeaders = LogHeaders,
                LogPayloads = LogPayloads
            };
            clonedOptions.ModelTypeAssemblies.AddRange(ModelTypeAssemblies);

            return clonedOptions;
        }

        /// <summary>
        ///     Ensure that the <see cref="KubeClientOptions"/> are valid.
        /// </summary>
        /// <returns>
        ///     The <see cref="KubeClientOptions"/> (enables inline use).
        /// </returns>
        public KubeClientOptions EnsureValid()
        {
            if (ApiEndPoint == null || !ApiEndPoint.IsAbsoluteUri)
                throw new KubeClientException("Invalid KubeClientOptions: must specify a valid API end-point.");

            if (ClientCertificate != null && !ClientCertificate.HasPrivateKey)
                throw new KubeClientException("Invalid KubeClientOptions: the private key for the supplied client certificate is not available.");

            if (String.IsNullOrWhiteSpace(KubeNamespace))
                throw new KubeClientException("Invalid KubeClientOptions: must specify a valid default namespace.");

            return this;
        }

        /// <summary>
        ///     Create new <see cref="KubeClientOptions"/> using pod-level configuration.
        /// </summary>
        /// <returns>
        ///     The configured <see cref="KubeClientOptions"/>.
        /// </returns>
        /// <remarks>
        ///     Only works from within a container running in a Kubernetes Pod.
        /// </remarks>
        public static KubeClientOptions FromPodServiceAccount()
        {
            string kubeServiceHost = Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST");
            string kubeServicePort = Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_PORT");
            if (String.IsNullOrWhiteSpace(kubeServiceHost) || String.IsNullOrWhiteSpace(kubeServicePort))
                throw new InvalidOperationException("KubeApiClient.CreateFromPodServiceAccount can only be called when running in a Kubernetes Pod (KUBERNETES_SERVICE_HOST and/or KUBERNETES_SERVICE_PORT environment variable is not defined).");

            string apiEndPoint = $"https://{kubeServiceHost}:{kubeServicePort}/";
            string accessToken = File.ReadAllText("/var/run/secrets/kubernetes.io/serviceaccount/token");
            var kubeCACertificate = new X509Certificate2(
                File.ReadAllBytes("/var/run/secrets/kubernetes.io/serviceaccount/ca.crt")
            );

            return new KubeClientOptions
            {
                ApiEndPoint = new Uri(apiEndPoint),
                AuthStrategy = KubeAuthStrategy.BearerToken,
                AccessToken = accessToken,
                CertificationAuthorityCertificate = kubeCACertificate
            };
        }
    }

    /// <summary>
    ///     Represents a strategy for authenticating to the Kubernetes API.
    /// </summary>
    public enum KubeAuthStrategy
    {
        /// <summary>
        ///     No authentication (e.g. via "kubectl proxy").
        /// </summary>
        None,

        /// <summary>
        ///     Client certificate (i.e. mutual SSL) authentication.
        /// </summary>
        ClientCertificate,

        /// <summary>
        ///     A pre-defined (static) bearer token.
        /// </summary>
        BearerToken,

        /// <summary>
        ///     A bearer token obtained by an authentication provider (i.e. running an external command).
        /// </summary>
        BearerTokenProvider
    }
}
