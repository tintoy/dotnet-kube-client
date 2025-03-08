using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaProps is a JSON-Schema following Specification Draft 4 (http://json-schema.org/).
    /// </summary>
    public partial class JSONSchemaPropsV1
    {
        /// <summary>
        ///     x-kubernetes-patch-merge-strategy annotates an object to describe the strategy used when performing a patch-merge.
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-patch-strategy")]
        [JsonProperty("x-kubernetes-patch-strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string KubernetesPatchMergeStrategy { get; set; }

        /// <summary>
        ///     x-kubernetes-patch-merge-key annotates an object to describe the key used when performing a patch-merge.
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-patch-merge-key")]
        [JsonProperty("x-kubernetes-patch-merge-key", NullValueHandling = NullValueHandling.Ignore)]
        public string KubernetesPatchMergeKey { get; set; }
    }
}
