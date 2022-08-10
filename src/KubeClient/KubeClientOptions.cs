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
        ///     The strategy used for authenticating to the Kubernetes API.
        /// </summary>
        public KubeAuthStrategy AuthStrategy { get; set; }

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
        ///     Environment variables passed to external commands
        /// </summary>
        public Dictionary<string, string> EnvironmentVariables { get; set; }

        /// <summary>
        /// Create a copy of the <see cref="KubeClientOptions"/>.
        /// </summary>
        /// <returns>The new <see cref="KubeClientOptions"/>.</returns>
        public KubeClientOptions Clone()
        {
            var clonedOptions = new KubeClientOptions
            {
                AllowInsecure = AllowInsecure,
                ApiEndPoint = ApiEndPoint,
                AuthStrategy = AuthStrategy,
                CertificationAuthorityCertificate = CertificationAuthorityCertificate,
                KubeNamespace = KubeNamespace,
                LoggerFactory = LoggerFactory,
                LogHeaders = LogHeaders,
                LogPayloads = LogPayloads,
                EnvironmentVariables = EnvironmentVariables
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

            if (String.IsNullOrWhiteSpace(KubeNamespace))
                throw new KubeClientException("Invalid KubeClientOptions: must specify a valid default namespace.");

            return this;
        }

        /// <summary>
        ///     Create new <see cref="KubeClientOptions"/> using pod-level configuration. 
        /// </summary>
        /// <param name="serviceAccountPath">
        ///     The location of the volume containing service account token, CA certificate, and default namespace.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeClientOptions"/>.
        /// </returns>
        /// <remarks>
        ///     Only works from within a container running in a Kubernetes Pod.
        /// </remarks>
        /// <exception cref="InvalidOperationException"></exception>
        public static KubeClientOptions FromPodServiceAccount(string serviceAccountPath = KubeClientConstants.DefaultServiceAccountPath)
        {
            string kubeServiceHost = Environment.GetEnvironmentVariable(KubeClientConstants.KubernetesServiceHost);
            string kubeServicePort = Environment.GetEnvironmentVariable(KubeClientConstants.KubernetesServicePort);
            if (String.IsNullOrWhiteSpace(kubeServiceHost) || String.IsNullOrWhiteSpace(kubeServicePort))
                throw new InvalidOperationException($"KubeApiClient.CreateFromPodServiceAccount can only be called when running in a Kubernetes Pod ({KubeClientConstants.KubernetesServiceHost} and/or {KubeClientConstants.KubernetesServicePort} environment variable is not defined).");

            string defaultNamespace = File.ReadAllText(Path.Combine(serviceAccountPath, "namespace")).Trim();

            string apiEndPoint = $"https://{kubeServiceHost}:{kubeServicePort}/";
            string accessToken = File.ReadAllText(Path.Combine(serviceAccountPath, "token"));
            var kubeCACertificate = new X509Certificate2(
                File.ReadAllBytes(Path.Combine(serviceAccountPath, "ca.crt"))
            );

            return new KubeClientOptions
            {
                ApiEndPoint = new Uri(apiEndPoint),
                AuthStrategy = KubeAuthStrategy.BearerToken(accessToken),
                CertificationAuthorityCertificate = kubeCACertificate,
                KubeNamespace = defaultNamespace
            };
        }
    }
}
