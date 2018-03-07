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
        ///     Add <see cref="KubeClientOptions"/> from local Kubernetes client configuration.
        /// </summary>
        /// <param name="services">
        ///     The service collection to configure.
        /// </param>
        /// <param name="kubeConfigFileName">
        ///     The optional name of a specific configuration file to use (defaults to "$HOME/.kube/config").
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

            if (String.IsNullOrWhiteSpace(kubeConfigFileName))
            {
                kubeConfigFileName = Path.GetFullPath(Path.Combine(
                    Environment.GetEnvironmentVariable("HOME"),
                    ".kube",
                    "config"
                ));
            }
            FileInfo kubeConfigFile = new FileInfo(kubeConfigFileName);

            // IOptions<KubeClientOptions>
            services.Configure<KubeClientOptions>(kubeClientOptions =>
            {
                Config.Load(kubeConfigFile).ConfigureKubeClientOptions(kubeClientOptions, kubeContextName, defaultKubeNamespace);
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
        ///     The optional name of a specific configuration file to use (defaults to "$HOME/.kube/config").
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

            if (String.IsNullOrWhiteSpace(kubeConfigFileName))
            {
                kubeConfigFileName = Path.GetFullPath(Path.Combine(
                    Environment.GetEnvironmentVariable("HOME"),
                    ".kube",
                    "config"
                ));
            }
            FileInfo kubeConfigFile = new FileInfo(kubeConfigFileName);

            services.AddKubeClientOptions(name, kubeClientOptions =>
            {
                Config.Load(kubeConfigFile).ConfigureKubeClientOptions(kubeClientOptions, kubeContextName, defaultKubeNamespace);
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
        ///     The optional name of a specific configuration file to use (defaults to "$HOME/.kube/config").
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

            if (String.IsNullOrWhiteSpace(kubeConfigFileName))
            {
                kubeConfigFileName = Path.GetFullPath(Path.Combine(
                    Environment.GetEnvironmentVariable("HOME"),
                    ".kube",
                    "config"
                ));
            }
            FileInfo kubeConfigFile = new FileInfo(kubeConfigFileName);

            Config config = Config.Load(kubeConfigFile);
            foreach (Context targetContext in config.Contexts) // AF: List of contexts is static for application lifetime, but config for those contexts is dynamic.
            {
                services.AddKubeClientOptions(targetContext.Name, kubeClientOptions =>
                {
                    Config.Load(kubeConfigFile).ConfigureKubeClientOptions(kubeClientOptions, targetContext.Name, defaultKubeNamespace);
                });
            }

            return services;
        }
    }
}
