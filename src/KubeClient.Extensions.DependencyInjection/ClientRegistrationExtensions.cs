using Microsoft.Extensions.DependencyInjection;
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
                services.AddScoped<KubeApiClient>(
                    serviceProvider => KubeApiClient.CreateFromPodServiceAccount()
                );
            }
            else
            {
                services.AddScoped<KubeApiClient>(serviceProvider =>
                {
                    KubeClientOptions kubeOptions = serviceProvider.GetRequiredService<IOptions<KubeClientOptions>>().Value;

                    if (String.IsNullOrWhiteSpace(kubeOptions.ApiEndPoint))
                        throw new InvalidOperationException("Application configuration is missing Kubernetes API end-point.");

                    KubeApiClient client = null;

                    if (!String.IsNullOrWhiteSpace(kubeOptions.Token))
                    {
                        client = KubeApiClient.Create(
                            endPointUri: new Uri(kubeOptions.ApiEndPoint),
                            accessToken: kubeOptions.Token,
                            expectServerCertificate: kubeOptions.CertificationAuthorityCertificate
                        );
                    }
                    else if (kubeOptions.ClientCertificate != null)
                    {
                        client = KubeApiClient.Create(
                            endPointUri: new Uri(kubeOptions.ApiEndPoint),
                            clientCertificate: kubeOptions.ClientCertificate,
                            expectServerCertificate: kubeOptions.CertificationAuthorityCertificate
                        );
                    }
                    else
                        throw new InvalidOperationException($"KubeClientOptions for '{kubeOptions.ApiEndPoint}' do not contain valid credentials.");

                    client.DefaultNamespace = kubeOptions.KubeNamespace;

                    return client;
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

            services.AddScoped<KubeApiClient>(serviceProvider =>
            {
                var client = KubeApiClient.Create(endPointUri, kubeOptions.Token);
                client.DefaultNamespace = kubeOptions.KubeNamespace;

                return client;
            });
        }
    }
}
