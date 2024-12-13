namespace KubeClient.ResourceClients
{
    /// <summary>
    ///     A client for a specific Kubernetes resource API.
    /// </summary>
    public interface IKubeResourceClient
    {
        /// <summary>
        ///     The Kubernetes API client.
        /// </summary>
        IKubeApiClient KubeClient { get; }
    }
}