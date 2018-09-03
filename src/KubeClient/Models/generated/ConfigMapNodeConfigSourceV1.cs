using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ConfigMapNodeConfigSource contains the information to reference a ConfigMap as a config source for the Node.
    /// </summary>
    public partial class ConfigMapNodeConfigSourceV1
    {
        /// <summary>
        ///     UID is the metadata.UID of the referenced ConfigMap. This field is forbidden in Node.Spec, and required in Node.Status.
        /// </summary>
        [JsonProperty("uid")]
        [YamlMember(Alias = "uid")]
        public string Uid { get; set; }

        /// <summary>
        ///     Name is the metadata.name of the referenced ConfigMap. This field is required in all cases.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace is the metadata.namespace of the referenced ConfigMap. This field is required in all cases.
        /// </summary>
        [JsonProperty("namespace")]
        [YamlMember(Alias = "namespace")]
        public string Namespace { get; set; }

        /// <summary>
        ///     ResourceVersion is the metadata.ResourceVersion of the referenced ConfigMap. This field is forbidden in Node.Spec, and required in Node.Status.
        /// </summary>
        [JsonProperty("resourceVersion")]
        [YamlMember(Alias = "resourceVersion")]
        public string ResourceVersion { get; set; }

        /// <summary>
        ///     KubeletConfigKey declares which key of the referenced ConfigMap corresponds to the KubeletConfiguration structure This field is required in all cases.
        /// </summary>
        [JsonProperty("kubeletConfigKey")]
        [YamlMember(Alias = "kubeletConfigKey")]
        public string KubeletConfigKey { get; set; }
    }
}
