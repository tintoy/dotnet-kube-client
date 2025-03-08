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
        ///     Default path of mounted volume containing Kubernetes service account token, CA certificate, and default namespace.
        /// </summary>
        public const string DefaultServiceAccountPath = "/var/run/secrets/kubernetes.io/serviceaccount";
    }
}