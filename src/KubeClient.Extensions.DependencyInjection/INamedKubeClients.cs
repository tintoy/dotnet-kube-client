using Microsoft.Extensions.DependencyInjection;

namespace KubeClient
{
    /// <summary>
    ///     Represents a service for resolving named Kubernetes clients.
    /// </summary>
    public interface INamedKubeClients
    {
        /// <summary>
        ///     Resolve the Kubernetes API client with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The client name.
        /// </param>
        /// <returns>
        ///     The resolved <see cref="KubeApiClient"/>.
        /// </returns>
        IKubeApiClient Get(string name);
    }
}
