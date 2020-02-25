using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient
{
    /// <summary>
    ///     Constants for the Kubernetes API client.
    /// </summary>
    public static class KubeClientConstants
    {
        /// <summary>
        ///     Environment Variable set in a Kubernetes Pod containing the host name of the API Service
        /// </summary>
        public const string KubernetesServiceHost = "KUBERNETES_SERVICE_HOST";
        
        /// <summary>
        ///     Environment Variable set in a Kubernetes Pod containing the port of the API Service
        /// </summary>
        public const string KubernetesServicePort = "KUBERNETES_SERVICE_PORT";

        /// <summary>
        ///     Default path of mounted volume containing Kubernetes service account token and CA certificate
        /// </summary>
        public const string DefaultServiceAccountPath = "/var/run/secrets/kubernetes.io/serviceaccount";
    }
}