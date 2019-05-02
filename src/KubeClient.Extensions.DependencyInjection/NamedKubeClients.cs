using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace KubeClient
{
    /// <summary>
    ///     A service for resolving named Kubernetes clients.
    /// </summary>
    public class NamedKubeClients
        : INamedKubeClients
    {
        /// <summary>
        ///     Create a new <see cref="NamedKubeClients"/> service.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The underlying service provider used to resolve required services.
        /// </param>
        public NamedKubeClients(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        ///     The underlying service provider used to resolve required services.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        ///     Resolve the Kubernetes API client with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The client name.
        /// </param>
        /// <returns>
        ///     The resolved <see cref="KubeApiClient"/>.
        /// </returns>
        public IKubeApiClient Get(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            KubeClientOptions clientOptions = ServiceProvider.GetRequiredService<IOptionsMonitor<KubeClientOptions>>().Get(name);
            if (clientOptions == null)
                throw new InvalidOperationException($"Cannot resolve a {nameof(KubeClientOptions)} instance named '{name}'.");

            clientOptions.LoggerFactory = ServiceProvider.GetService<ILoggerFactory>();

            return KubeApiClient.Create(clientOptions);
        }
    }
}
