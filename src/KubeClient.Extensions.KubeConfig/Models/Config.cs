using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     Kubernetes client configuration.
    /// </summary>
    public class Config
    {
        /// <summary>
        ///     The client configuration API version (should be "v1").
        /// </summary>
        [YamlMember(Alias = "apiVersion")]
        public string ApiVersion { get; set ;}

        /// <summary>
        ///     The client configuration kind (should be "Configuration").
        /// </summary>
        [YamlMember(Alias = "kind")]
        public string Kind { get; set; }

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
        ///     Load and parse configuration from the specified file (usually ~/.kube/config).
        /// </summary>
        /// <param name="configFile">
        ///     A <see cref="FileInfo"/> representing the configuration file.
        /// </param>
        /// <returns>
        ///     The parsed configuration.
        /// </returns>
        public static Config Load(FileInfo configFile)
        {
            if (configFile == null)
                throw new ArgumentNullException(nameof(configFile));
            
            Deserializer deserializer = new DeserializerBuilder()
                .IgnoreUnmatchedProperties()
                .Build();

            using (StreamReader configReader = configFile.OpenText())
            {
                return deserializer.Deserialize<Config>(configReader);
            }
        }

        /// <summary>
        ///     Create <see cref="KubeClientOptions"/> from the settings specified in the <see cref="Config"/>.
        /// </summary>
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
        public KubeClientOptions ToKubeClientOptions(string kubeContextName = null, string defaultKubeNamespace = null)
        {
            return ConfigureKubeClientOptions(new KubeClientOptions(), kubeContextName, defaultKubeNamespace);
        }

        /// <summary>
        ///     Configure <see cref="KubeClientOptions"/> from the settings specified in the <see cref="Config"/>.
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
            kubeClientOptions.AccessToken = targetUser.Config.GetRawToken();

            return kubeClientOptions;
        }
    }
}
