using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     The base class for Kubernetes resources.
    /// </summary>
    public abstract class KubeResourceV1
        : KubeObjectV1
    {
        /// <summary>
        ///     Standard object's metadata. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#metadata
        /// </summary>
        [JsonProperty("metadata")]
        [YamlMember(Alias = "metadata")]
        public ObjectMetaV1 Metadata { get; set; }
    }
}
