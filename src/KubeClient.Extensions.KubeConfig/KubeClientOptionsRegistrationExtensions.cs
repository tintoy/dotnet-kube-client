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
        /// <returns>
        ///     The configured service collection.
        /// </returns>
        public static IServiceCollection AddKubeClientOptionsFromKubeConfig(this IServiceCollection services, string kubeConfigFileName = null, string kubeContextName = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

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
            services.AddScoped(serviceProvider =>
            {
                Config config = Config.Load(kubeConfigFile);

                string targetContextName = kubeContextName ?? config.CurrentContextName;
                if (String.IsNullOrWhiteSpace(targetContextName))
                    throw new InvalidOperationException("The kubeContextName parameter was not specified, and the Kubernetes client configuration does not specify a current context.");

                Context targetContext = config.Contexts.Find(context => context.Name == targetContextName);
                if (targetContext == null)
                    throw new InvalidOperationException($"Cannot find a context in the Kubernetes client configuration named '{targetContextName}'.");

                Cluster targetCluster = config.Clusters.Find(cluster => cluster.Name == targetContext.Config.ClusterName);
                if (targetCluster == null)
                    throw new InvalidOperationException($"Cannot find a cluster in the Kubernetes client configuration named '{targetContext.Config.ClusterName}'.");

                UserIdentity targetUser = config.UserIdentities.Find(user => user.Name == targetContext.Config.UserName);
                if (targetUser == null)
                    throw new InvalidOperationException($"Cannot find a user identity in the Kubernetes client configuration named '{targetContext.Config.UserName}'.");

                KubeClientOptions options = new KubeClientOptions
                {
                    ApiEndPoint = targetCluster.Config.Server,
                    ClientCertificate = targetUser.Config.GetClientCertificate(),
                    CertificationAuthorityCertificate = targetCluster.Config.GetCACertificate(),
                    Token = targetUser.Config.GetRawToken()
                };

                return Options.Create(options);
            });

            return services;
        }
    }
}
