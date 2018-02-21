using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace KubeClient.Extensions.DependencyInjection
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
                services.AddScoped<KubeClient.KubeApiClient>(
                    serviceProvider => KubeClient.KubeApiClient.CreateFromPodServiceAccount()
                );
            }
            else
            {
                services.AddScoped<KubeClient.KubeApiClient>(serviceProvider =>
                {
                    KubeClientOptions kubeOptions = serviceProvider.GetRequiredService<IOptions<KubeClientOptions>>().Value;

                    if (String.IsNullOrWhiteSpace(kubeOptions.ApiEndPoint))
                        throw new InvalidOperationException("Application configuration is missing Kubernetes API end-point.");

                    if (String.IsNullOrWhiteSpace(kubeOptions.Token))
                        throw new InvalidOperationException("Application configuration is missing Kubernetes API token.");

                    return KubeClient.KubeApiClient.Create(
                        endPointUri: new Uri(kubeOptions.ApiEndPoint),
                        accessToken: kubeOptions.Token
                    );
                });
            }
        }

        /// <summary>
        ///     Add a <see cref="KubeApiClient"/> to the service collection.
        /// </summary>
        /// <param name="services">
	        ///     The service collection to configure.
        /// </param>
        /// <param name="kubeOptions">
        ///     <see cref="KubeClientOptions"/> containing the client configuration to use.
        /// </param>
        public static void AddKubeClient(this IServiceCollection services, KubeClientOptions kubeOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            if (kubeOptions == null)
                throw new ArgumentNullException(nameof(kubeOptions));

            if (String.IsNullOrWhiteSpace(kubeOptions.ApiEndPoint))
                throw new ArgumentException("Application configuration is missing Kubernetes API end-point.", nameof(kubeOptions));

            if (String.IsNullOrWhiteSpace(kubeOptions.Token))
                throw new ArgumentException("Application configuration is missing Kubernetes API token.", nameof(kubeOptions));

            Uri endPointUri = new Uri(kubeOptions.ApiEndPoint);

            services.AddScoped<KubeClient.KubeApiClient>(
                serviceProvider => KubeClient.KubeApiClient.Create(endPointUri, kubeOptions.Token)
            );
        }
    }
}
