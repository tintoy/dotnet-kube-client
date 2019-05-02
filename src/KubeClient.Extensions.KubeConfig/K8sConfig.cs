using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using YamlDotNet.Serialization;

namespace KubeClient
{
    using Extensions.KubeConfig.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Kubernetes client configuration.
    /// </summary>
    public class K8sConfig
    {
        /// <summary>
        ///     The client configuration API version (should be "v1").
        /// </summary>
        [YamlMember(Alias = "apiVersion")]
        public string ApiVersion { get; set; } = "v1";

        /// <summary>
        ///     The client configuration kind (should be "Configuration").
        /// </summary>
        [YamlMember(Alias = "kind")]
        public string Kind { get; set; } = "Configuration";

        /// <summary>
        ///     The currently-selected Kubernetes context.
        /// </summary>
        [YamlMember(Alias = "current-context")]
        public string CurrentContextName { get; set; }

        /// <summary>
        ///     Kubernetes contexts.
        /// </summary>
        [YamlMember(Alias = "contexts")]
        public List<Context> Contexts { get; set; } = new List<Context>();

        /// <summary>
        ///     Kubernetes clusters.
        /// </summary>
        [YamlMember(Alias = "clusters")]
        public List<Cluster> Clusters { get; set; } = new List<Cluster>();

        /// <summary>
        ///     Kubernetes user identities.
        /// </summary>
        [YamlMember(Alias = "users")]
        public List<UserIdentity> UserIdentities { get; set; } = new List<UserIdentity>();

        /// <summary>
        ///     Locate the full path of the configuration file ~/.kube/config.
        /// </summary>
        /// <returns>
        ///     The full path of the config file.
        /// </returns>
        public static string Locate()
        {
            //Windows users in an AD domain with a Home Drive mapped will have the HOME environment variable set.
            //Mirror the logic that kubectl and other Kubernetes tools use for homedir resolution: https://github.com/kubernetes/kubernetes/blob/master/staging/src/k8s.io/client-go/util/homedir/homedir.go
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var home = Environment.GetEnvironmentVariable("HOME");
                if (!String.IsNullOrEmpty(home))
                    return Path.Combine(home, ".kube", "config");

                var homeDrive = Environment.GetEnvironmentVariable("HOMEDRIVE");
                var homePath = Environment.GetEnvironmentVariable("HOMEPATH");
                if (!String.IsNullOrEmpty(homeDrive) && !String.IsNullOrEmpty(homePath))
                    return Path.Combine(homeDrive + homePath, ".kube", "config");

                var userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
                if (!String.IsNullOrEmpty(userProfile))
                    return Path.Combine(userProfile, ".kube", "config");
            }

            return Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".kube", "config");
        }

        /// <summary>
        ///     Load and parse configuration from ~/.kube/config.
        /// </summary>
        /// <returns>
        ///     The parsed configuration.
        /// </returns>
        public static K8sConfig Load()
        {
            return Load(configFile: Locate());
        }

        /// <summary>
        ///     Load and parse configuration from the specified file (usually ~/.kube/config).
        /// </summary>
        /// <param name="configFile">
        ///     The path of the configuration file.
        /// </param>
        /// <returns>
        ///     The parsed configuration.
        /// </returns>
        public static K8sConfig Load(string configFile) => Load(new FileInfo(configFile));

        /// <summary>
        ///     Load and parse configuration from the specified file (usually ~/.kube/config).
        /// </summary>
        /// <param name="configFile">
        ///     A <see cref="FileInfo"/> representing the configuration file.
        /// </param>
        /// <returns>
        ///     The parsed configuration.
        /// </returns>
        public static K8sConfig Load(FileInfo configFile)
        {
            if (configFile == null)
                throw new ArgumentNullException(nameof(configFile));

            Deserializer deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();

            using (StreamReader configReader = configFile.OpenText())
            {
                return deserializer.Deserialize<K8sConfig>(configReader);
            }
        }

        /// <summary>
        ///     Create <see cref="KubeClientOptions"/> from the settings specified in the <see cref="K8sConfig"/>.
        /// </summary>
        /// <param name="kubeContextName">
        ///     The name of the Kubernetes context to use.
        /// 
        ///     If not specified, then the current context (as configured) will be used.
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default Kubernetes namespace to use.
        /// </param>
        /// <param name="loggerFactory">
        ///     An optional <see cref="ILoggerFactory"/> used to create loggers for client components.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeClientOptions"/>.
        /// </returns>
        public KubeClientOptions ToKubeClientOptions(string kubeContextName = null, string defaultKubeNamespace = null, ILoggerFactory loggerFactory = null)
        {
            var clientOptions = new KubeClientOptions { LoggerFactory = loggerFactory };

            return ConfigureKubeClientOptions(clientOptions, kubeContextName, defaultKubeNamespace);
        }

        /// <summary>
        ///     Configure <see cref="KubeClientOptions"/> from the settings specified in the <see cref="K8sConfig"/>.
        /// </summary>
        /// <param name="kubeClientOptions">
        ///     
        /// </param>
        /// <param name="kubeContextName">
        ///     The name of the Kubernetes context to use.
        /// 
        ///     If not specified, then the current context (as configured) will be used.
        /// </param>
        /// <param name="defaultKubeNamespace">
        ///     The default Kubernetes namespace to use.
        /// </param>
        /// <returns>
        ///     The configured <see cref="KubeClientOptions"/>.
        /// </returns>
        public KubeClientOptions ConfigureKubeClientOptions(KubeClientOptions kubeClientOptions, string kubeContextName = null, string defaultKubeNamespace = null)
        {
            if (kubeClientOptions == null)
                throw new ArgumentNullException(nameof(kubeClientOptions));

            string targetContextName = kubeContextName ?? CurrentContextName;
            if (String.IsNullOrWhiteSpace(targetContextName))
                throw new InvalidOperationException("The kubeContextName parameter was not specified, and the Kubernetes client configuration does not specify a current context.");

            Context targetContext = Contexts.Find(context => context.Name == targetContextName);
            if (targetContext == null)
                throw new InvalidOperationException($"Cannot find a context in the Kubernetes client configuration named '{targetContextName}'.");

            Cluster targetCluster = Clusters.Find(cluster => cluster.Name == targetContext.Config.ClusterName);
            if (targetCluster == null)
                throw new InvalidOperationException($"Cannot find a cluster in the Kubernetes client configuration named '{targetContext.Config.ClusterName}'.");

            UserIdentity targetUser = UserIdentities.Find(user => user.Name == targetContext.Config.UserName);
            if (targetUser == null)
                throw new InvalidOperationException($"Cannot find a user identity in the Kubernetes client configuration named '{targetContext.Config.UserName}'.");

            kubeClientOptions.ApiEndPoint = new Uri(targetCluster.Config.Server);
            kubeClientOptions.KubeNamespace = defaultKubeNamespace;
            kubeClientOptions.ClientCertificate = targetUser.Config.GetClientCertificate();
            kubeClientOptions.AllowInsecure = targetCluster.Config.AllowInsecure;
            kubeClientOptions.CertificationAuthorityCertificate = targetCluster.Config.GetCACertificate();

            // Mixed authentication types are not supported.
            if (kubeClientOptions.ClientCertificate == null)
            {
                string accessToken = targetUser.Config.GetRawToken();
                if (!String.IsNullOrWhiteSpace(accessToken))
                {
                    kubeClientOptions.AccessToken = accessToken;
                    kubeClientOptions.AuthStrategy = KubeAuthStrategy.BearerToken;
                }
                else
                {
                    kubeClientOptions.AuthStrategy = KubeAuthStrategy.None;
                }

                AuthProviderConfig authProvider = targetUser.Config.AuthProvider;
                if (authProvider != null)
                {
                    kubeClientOptions.AuthStrategy = KubeAuthStrategy.BearerTokenProvider;

                    if (authProvider.Config.TryGetValue("cmd-path", out object accessTokenCommand))
                        kubeClientOptions.AccessTokenCommand = (string)accessTokenCommand;

                    if (authProvider.Config.TryGetValue("cmd-args", out object accessTokenCommandArguments))
                        kubeClientOptions.AccessTokenCommandArguments = (string)accessTokenCommandArguments;

                    if (authProvider.Config.TryGetValue("token-key", out object accessTokenSelector))
                        kubeClientOptions.AccessTokenSelector = (string)accessTokenSelector;

                    if (authProvider.Config.TryGetValue("expiry-key", out object accessTokenExpirySelector))
                        kubeClientOptions.AccessTokenExpirySelector = (string)accessTokenExpirySelector;

                    if (authProvider.Config.TryGetValue("access-token", out object initialAccessToken))
                        kubeClientOptions.InitialAccessToken = (string)initialAccessToken;

                    if (authProvider.Config.TryGetValue("expiry", out object initialTokenExpiry))
                    {
                        kubeClientOptions.InitialTokenExpiryUtc = DateTime.Parse((string)initialTokenExpiry,
                            provider: CultureInfo.InvariantCulture,
                            styles: DateTimeStyles.AssumeUniversal
                        );
                    }
                }
            }
            else
                kubeClientOptions.AuthStrategy = KubeAuthStrategy.ClientCertificate;

            return kubeClientOptions;
        }
    }
}
