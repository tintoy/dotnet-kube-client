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
    }
}
