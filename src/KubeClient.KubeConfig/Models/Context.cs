using YamlDotNet.Serialization;

namespace KubeClient.KubeConfig.Models
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