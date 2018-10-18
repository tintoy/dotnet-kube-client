using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeConfigSource specifies a source of node configuration. Exactly one subfield (excluding metadata) must be non-nil.
    /// </summary>
    public partial class NodeConfigSourceV1
    {
        /// <summary>
        ///     ConfigMap is a reference to a Node's ConfigMap
        /// </summary>
        [YamlMember(Alias = "configMap")]
        [JsonProperty("configMap", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapNodeConfigSourceV1 ConfigMap { get; set; }
    }
}
