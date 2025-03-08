namespace KubeClient
{
    /// <summary>
    ///     Well-known annotation keys used by Kubernetes.
    /// </summary>
    public static class K8sAnnotations
    {
        /// <summary>
        ///     Well-known (deployment-specific) annotation keys used by Kubernetes.
        /// </summary>
        public static class Deployment
        {
            /// <summary>
            ///     The annotation key representing a specific revision of a Deployment.
            /// </summary>
            public const string Revision = "deployment.kubernetes.io/revision";
        }
    }
}