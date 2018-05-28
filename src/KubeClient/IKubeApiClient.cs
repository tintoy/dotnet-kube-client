using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace KubeClient
{
    using MessageHandlers;
    using ResourceClients;

    /// <summary>
    ///     Represents a client for the Kubernetes API.
    /// </summary>
    public interface IKubeApiClient
        : IDisposable
    {
        /// <summary>
        ///     The base address of the Kubernetes API end-point targeted by the client.
        /// </summary>
        Uri ApiEndPoint { get; }

        /// <summary>
        ///     The default Kubernetes namespace.
        /// </summary>
        string DefaultNamespace { get; set; }

        /// <summary>
        ///     The underlying HTTP client.
        /// </summary>
        HttpClient Http { get; }

        /// <summary>
        ///     The <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </summary>
        ILoggerFactory LoggerFactory { get; }

        /// <summary>
        ///     Get a copy of the <see cref="KubeClientOptions"/> used to configure the client.
        /// </summary>
        /// <returns>
        ///     The <see cref="KubeClientOptions"/>.
        /// </returns>
        KubeClientOptions GetClientOptions();

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
        TClient ResourceClient<TClient>(Func<IKubeApiClient, TClient> clientFactory)
            where TClient : IKubeResourceClient;
    }
}
