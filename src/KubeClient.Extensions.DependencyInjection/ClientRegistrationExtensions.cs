using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace KubeClient
{
    /// <summary>
    ///     Extension methods for registering <see cref="KubeApiClient"/> as a component.
    /// </summary>
    public static class ClientRegistrationExtensions
    {
        /// <summary>
        ///     Add a <see cref="KubeApiClient"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="usePodServiceAccount">
        ///     Configure the client to use the service account for the current Pod?
        /// </param>
        public static void AddKubeClient(this IServiceCollection services, bool usePodServiceAccount = false)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            if (usePodServiceAccount)
            {
                // When running inside Kubernetes, use pod-level service account (e.g. access token from mounted Secret).
                services.AddScoped<KubeApiClient>(serviceProvider =>
                {
                    ILogger logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger(nameof(KubeApiClient));

                    return KubeApiClient.CreateFromPodServiceAccount(logger);
                });
            }
            else
            {
                services.AddScoped<KubeApiClient>(serviceProvider =>
                {
                    KubeClientOptions options = serviceProvider.GetRequiredService<IOptions<KubeClientOptions>>().Value;
                    ILogger logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger(nameof(KubeApiClient));

                    return KubeApiClient.Create(options, logger);
                });
            }
        }

        /// <summary>
        ///     Add a <see cref="KubeApiClient"/> to the service collection.
        /// </summary>
        /// <param name="services">
	        ///     The service collection to configure.
        /// </param>
        /// <param name="options">
        ///     <see cref="KubeClientOptions"/> containing the client configuration to use.
        /// </param>
        public static void AddKubeClient(this IServiceCollection services, KubeClientOptions options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.EnsureValid();

            services.AddScoped<KubeApiClient>(serviceProvider =>
            {
                ILogger logger = serviceProvider.GetService<ILoggerFactory>()?.CreateLogger(nameof(KubeApiClient));
                
                return KubeApiClient.Create(options, logger);
            });
        }
    }
}
