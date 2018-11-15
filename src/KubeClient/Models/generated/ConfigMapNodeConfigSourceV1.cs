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
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        /// <summary>
        ///     Name is the metadata.name of the referenced ConfigMap. This field is required in all cases.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace is the metadata.namespace of the referenced ConfigMap. This field is required in all cases.
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Include)]
        public string Namespace { get; set; }

        /// <summary>
        ///     ResourceVersion is the metadata.ResourceVersion of the referenced ConfigMap. This field is forbidden in Node.Spec, and required in Node.Status.
        /// </summary>
        [YamlMember(Alias = "resourceVersion")]
        [JsonProperty("resourceVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceVersion { get; set; }

        /// <summary>
        ///     KubeletConfigKey declares which key of the referenced ConfigMap corresponds to the KubeletConfiguration structure This field is required in all cases.
        /// </summary>
        [YamlMember(Alias = "kubeletConfigKey")]
        [JsonProperty("kubeletConfigKey", NullValueHandling = NullValueHandling.Include)]
        public string KubeletConfigKey { get; set; }
    }
}
