using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     A Kubernetes context links a cluster with a specific user identity.
    /// </summary>
    public class Context
    {
        /// <summary>
        ///     The context name.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     The context configuration.
        /// </summary>
        [YamlMember(Alias = "context")]
        public ContextConfig Config { get; set; } = new ContextConfig();
    }

    /// <summary>
    ///     Configuration for a Kubernetes client context.
    /// </summary>
    public class ContextConfig
    {
        /// <summary>
        ///     The name of the target cluster.
        /// </summary>
        [YamlMember(Alias = "cluster")]
        public string ClusterName { get; set; }

        /// <summary>
        ///     The name of the user identity to use.
        /// </summary>
        [YamlMember(Alias = "user")]
        public string UserName { get; set; }
    }
}