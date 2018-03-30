using Microsoft.Extensions.Logging;
using System;

namespace KubeClient
{
    /// <summary>
    ///     Logging-related extension methods for <see cref="KubeApiClient"/>.
    /// </summary>
    public static class KubeApiClientLoggingExtensions
    {
        /// <summary>
        ///     Get the <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </summary>
        /// <param name="kubeApiClient">
        ///     The <see cref="KubeApiClient"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="ILoggerFactory"/>.
        /// </returns>
        public static ILoggerFactory LoggerFactory(this KubeApiClient kubeApiClient)
        {
            if (kubeApiClient == null)
                throw new ArgumentNullException(nameof(kubeApiClient));
            
            return kubeApiClient.LoggerFactory;
        }
    }
}
