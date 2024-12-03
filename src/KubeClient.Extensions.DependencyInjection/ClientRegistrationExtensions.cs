using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

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
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClient(this IServiceCollection services, bool usePodServiceAccount = false)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (usePodServiceAccount)
            {
                services.AddScoped<KubeApiClient>(ResolveWithPodServiceAccount);
                services.AddScopedPassThrough<IKubeApiClient, KubeApiClient>();
            }
            else
            {
                services.AddScoped<KubeApiClient>(ResolveWithDefaultOptions);
                services.AddScopedPassThrough<IKubeApiClient, KubeApiClient>();
            }

            return services;
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
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClient(this IServiceCollection services, KubeClientOptions options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.EnsureValid();

            services.AddScoped<KubeApiClient>(
                BuildResolverWithOptions(options)
            );
            services.AddScopedPassThrough<IKubeApiClient, KubeApiClient>();

            return services;
        }

        /// <summary>
        ///     Add a <see cref="KubeApiClient" /> to the service collection. Automatically use a pod service account if no API endpoint is configured.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="configuration">
        ///     Configuration to be deserialized as <see cref="KubeClientOptions"/>.
        /// </param>
        public static IServiceCollection AddKubeClient(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new KubeClientOptions();
            configuration.Bind(options);

            if (options.ApiEndPoint == null)
            {
                options = KubeClientOptions.FromPodServiceAccount();
                configuration.Bind(options);
            }

            services.AddKubeClient(options);

            return services;
        }

        /// <summary>
        ///     Add a named <see cref="KubeApiClient"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="name">
        ///     A name used to resolve the Kubernetes client.
        /// </param>
        /// <param name="configure">
        ///     A delegate that performs required configuration of the <see cref="KubeClientOptions"/> to use.
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClient(this IServiceCollection services, string name, Action<KubeClientOptions> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (configure == null)
                throw new ArgumentNullException(nameof(configure));
            
            services.AddKubeClientOptions(name, configure);
            services.AddNamedKubeClients();

            return services;
        }

        /// <summary>
        ///     Add support for named Kubernetes client instances.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddNamedKubeClients(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            
            if (!services.Any(service => service.ServiceType == typeof(NamedKubeClients)))
                services.AddScoped<INamedKubeClients, NamedKubeClients>();

            return services;
        }

        /// <summary>
        ///     When running inside Kubernetes, resolve a <see cref="KubeApiClient"/> using the pod-level service account (e.g. access token from mounted Secret).
        /// </summary>
        /// <param name="serviceProvider">
        ///     The service provider used to resolve the <see cref="KubeApiClient"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="KubeApiClient"/>.
        /// </returns>
        static KubeApiClient ResolveWithPodServiceAccount(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            return KubeApiClient.CreateFromPodServiceAccount(
                loggerFactory: serviceProvider.GetService<ILoggerFactory>()
            );
        }

        /// <summary>
        ///     Resolve a <see cref="KubeApiClient"/> using the default registered options.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The service provider used to resolve the <see cref="KubeApiClient"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="KubeApiClient"/>.
        /// </returns>
        static KubeApiClient ResolveWithDefaultOptions(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            KubeClientOptions clientOptions = serviceProvider.GetRequiredService<IOptionsMonitor<KubeClientOptions>>().CurrentValue;
            if (clientOptions.LoggerFactory == null)
                clientOptions.LoggerFactory = serviceProvider.GetService<ILoggerFactory>();

            return KubeApiClient.Create(clientOptions);
        }

        /// <summary>
        ///     Build a delegate that resolves a <see cref="KubeApiClient"/> using the specified <see cref="KubeClientOptions"/>.
        /// </summary>
        /// <param name="options">
        ///     The <see cref="KubeClientOptions"/>.
        /// </param>
        /// <returns>
        ///     The resolver delegate.
        /// </returns>
        static Func<IServiceProvider, KubeApiClient> BuildResolverWithOptions(KubeClientOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            KubeClientOptions optionsSnapshot = options.EnsureValid().Clone();

            return (IServiceProvider serviceProvider) =>
            {
                KubeClientOptions clientOptions = optionsSnapshot.Clone();
                if (clientOptions.LoggerFactory == null)
                    clientOptions.LoggerFactory = serviceProvider.GetService<ILoggerFactory>();

                return KubeApiClient.Create(options);
            };
        }

        /// <summary>
        ///     Register a scoped service that resolves another service as its implementation.
        /// </summary>
        /// <typeparam name="TService">
        ///     The service type.
        /// </typeparam>
        /// <typeparam name="TImplementation">
        ///     The implementation type to resolve when the service is resolved.
        /// </typeparam>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        static IServiceCollection AddScopedPassThrough<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddScoped<TService>(
                serviceProvider => serviceProvider.GetRequiredService<TImplementation>()
            );
        }
    }
}
