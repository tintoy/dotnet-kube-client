using YamlDotNet.Serialization;

namespace KubeClient.Extensions.KubeConfig.Models
{
    /// <summary>
    ///     A Kubernetes cluster that clients can connect to.
    /// </summary>
    public class Cluster
    {
        /// <summary>
        ///     The cluster name.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     The cluster configuration.
        /// </summary>
        [YamlMember(Alias = "cluster")]
        public ClusterConfig Config { get; set; }
    }
}