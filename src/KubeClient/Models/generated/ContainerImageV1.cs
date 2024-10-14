using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Describe a container image
    /// </summary>
    public partial class ContainerImageV1
    {
        /// <summary>
        ///     Names by which this image is known. e.g. ["kubernetes.example/hyperkube:v1.0.7", "cloud-vendor.registry.example/cloud-vendor/hyperkube:v1.0.7"]
        /// </summary>
        [YamlMember(Alias = "names")]
        [JsonProperty("names", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Names { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Names"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNames() => Names.Count > 0;

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        [YamlMember(Alias = "sizeBytes")]
        [JsonProperty("sizeBytes", NullValueHandling = NullValueHandling.Ignore)]
        public long? SizeBytes { get; set; }
    }
}
