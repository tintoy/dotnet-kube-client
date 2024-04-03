using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace KubeClient
{
    using Extensions.KubeConfig.Models;

    /// <summary>
    ///     Extension methods for registering Kubernetes client options.
    /// </summary>
    public static class KubeClientOptionsRegistrationExtensions
    {
        /// <summary>
        ///     Add <see cref="KubeClientOptions"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="configure">
        ///     A delegate that performs required configuration of the <see cref="KubeClientOptions"/>.
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptions(this IServiceCollection services, Action<KubeClientOptions> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            services.Configure<KubeClientOptions>(options =>
            {
                configure(options);
                options.EnsureValid();
            });

            return services;
        }

        /// <summary>
        ///     Add named <see cref="KubeClientOptions"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="name">
        ///     A name used to resolve the options.
        /// </param>
        /// <param name="configure">
        ///     A delegate that performs required configuration of the <see cref="KubeClientOptions"/>.
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptions(this IServiceCollection services, string name, Action<KubeClientOptions> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            services.Configure<KubeClientOptions>(name, options =>
            {
                configure(options);
                options.EnsureValid();
            });

            return services;
        }

        /// <summary>
        ///     Add <see cref="KubeClientOptions"/> from local Kubernetes client configuration.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="kubeConfigFileName">
        ///     The optional name of a specific configuration file to use (if not specified, defaults to the path returned by <see cref="K8sConfig.Locate"/>).
        /// </param>
        /// <param name="kubeContextName">
        ///     The optional name of a specific Kubernetes client context to use.
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default namespace to use (if not specified, "default" is used).
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptionsFromKubeConfig(this IServiceCollection services, string kubeConfigFileName = null, string kubeContextName = null, string defaultKubeNamespace = "default")
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(defaultKubeNamespace))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'defaultNamespace'.", nameof(defaultKubeNamespace));

            FileInfo kubeConfigFile = GetKubeConfigFile(kubeConfigFileName);

            // IOptions<KubeClientOptions>
            services.Configure<KubeClientOptions>(kubeClientOptions =>
            {
                K8sConfig config = K8sConfig.Load(kubeConfigFile);
                KubeClientOptions optionsForTargetContext = config.ToKubeClientOptions(kubeContextName, defaultKubeNamespace);

                optionsForTargetContext.CopyTo(kubeClientOptions);

                kubeClientOptions.KubeNamespace = defaultKubeNamespace;
            });

            return services;
        }

        /// <summary>
        ///     Add named <see cref="KubeClientOptions"/> from local Kubernetes client configuration.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="name">
        ///     The name used to resolve these options.
        /// </param>
        /// <param name="kubeConfigFileName">
        ///     The optional name of a specific configuration file to use (if not specified, defaults to the path returned by <see cref="K8sConfig.Locate"/>).
        /// </param>
        /// <param name="kubeContextName">
        ///     The optional name of a specific Kubernetes client context to use.
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default namespace to use (if not specified, "default" is used).
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptionsFromKubeConfig(this IServiceCollection services, string name, string kubeConfigFileName = null, string kubeContextName = null, string defaultKubeNamespace = "default")
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (String.IsNullOrWhiteSpace(defaultKubeNamespace))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'defaultNamespace'.", nameof(defaultKubeNamespace));

            FileInfo kubeConfigFile = GetKubeConfigFile(kubeConfigFileName);

            services.AddKubeClientOptions(name, kubeClientOptions =>
            {
                K8sConfig config = K8sConfig.Load(kubeConfigFile);
                KubeClientOptions optionsForTargetContext = config.ToKubeClientOptions(kubeContextName, defaultKubeNamespace);

                optionsForTargetContext.CopyTo(kubeClientOptions);

                kubeClientOptions.KubeNamespace = defaultKubeNamespace;
            });

            return services;
        }

        /// <summary>
        ///     Add a named <see cref="KubeClientOptions"/> for each context in the local Kubernetes client configuration.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="kubeConfigFileName">
        ///     The optional name of a specific configuration file to use (if not specified, defaults to the path returned by <see cref="K8sConfig.Locate"/>).
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default namespace to use (if not specified, "default" is used).
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptionsFromKubeConfig(this IServiceCollection services, string kubeConfigFileName = null, string defaultKubeNamespace = "default")
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(defaultKubeNamespace))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'defaultNamespace'.", nameof(defaultKubeNamespace));

            FileInfo kubeConfigFile = GetKubeConfigFile(kubeConfigFileName);

            // List of contexts is static for application lifetime...
            K8sConfig config = K8sConfig.Load(kubeConfigFile);

            foreach (Context targetContext in config.Contexts)
            {
                services.AddKubeClientOptions(targetContext.Name, kubeClientOptions =>
                {
                    // ...but config for those contexts is dynamic.
                    K8sConfig currentConfig = K8sConfig.Load(kubeConfigFile);

                    KubeClientOptions optionsForTargetContext = currentConfig.ToKubeClientOptions(targetContext.Name, defaultKubeNamespace);
                    optionsForTargetContext.CopyTo(kubeClientOptions);

                    kubeClientOptions.KubeNamespace = defaultKubeNamespace;
                });
            }

            return services;
        }

        /// <summary>
        ///     Add <see cref="KubeClientOptions"/> from the pod service account.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default namespace to use (if not specified, "default" is used).
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptionsFromPodServiceAccount(this IServiceCollection services, string defaultKubeNamespace = "default")
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(defaultKubeNamespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(defaultKubeNamespace)}.", nameof(defaultKubeNamespace));

            services.AddKubeClientOptions(kubeClientOptions =>
            {
                KubeClientOptions fromPodServiceAccount = KubeClientOptions.FromPodServiceAccount();
                fromPodServiceAccount.CopyTo(kubeClientOptions);

                kubeClientOptions.KubeNamespace = defaultKubeNamespace;
            });

            return services;
        }

        /// <summary>
        ///     Add named <see cref="KubeClientOptions"/> from the pod service account.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="name">
        ///     The name used to resolve these options.
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default namespace to use (if not specified, "default" is used).
        /// </param>
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptionsFromPodServiceAccount(this IServiceCollection services, string name, string defaultKubeNamespace = "default")
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(name)}.", nameof(name));

            if (String.IsNullOrWhiteSpace(defaultKubeNamespace))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(defaultKubeNamespace)}.", nameof(defaultKubeNamespace));

            services.AddKubeClientOptions(name, kubeClientOptions =>
            {
                KubeClientOptions fromPodServiceAccount = KubeClientOptions.FromPodServiceAccount();
                fromPodServiceAccount.CopyTo(kubeClientOptions);

                kubeClientOptions.KubeNamespace = defaultKubeNamespace;
            });

            return services;
        }

        /// <summary>
        ///     Get a <see cref="FileInfo"/> representing a Kubernetes client configuration file.
        /// </summary>
        /// <param name="kubeConfigFileName">
        ///     The full path to the configuration file, or <c>null</c> to use the default config file (via <see cref="K8sConfig.Locate"/>).
        /// </param>
        /// <returns>
        ///     A <see cref="FileInfo"/> representing the configuration file.
        /// </returns>
        static FileInfo GetKubeConfigFile(string kubeConfigFileName)
        {
            if (String.IsNullOrWhiteSpace(kubeConfigFileName))
                kubeConfigFileName = K8sConfig.Locate();

            return new FileInfo(kubeConfigFileName);
        }
    }
}
