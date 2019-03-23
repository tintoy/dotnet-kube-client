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
        ///     Names by which this image is known. e.g. ["k8s.gcr.io/hyperkube:v1.0.7", "dockerhub.io/google_containers/hyperkube:v1.0.7"]
        /// </summary>
        [YamlMember(Alias = "names")]
        [JsonProperty("names", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Names { get; } = new List<string>();

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        [YamlMember(Alias = "sizeBytes")]
        [JsonProperty("sizeBytes", NullValueHandling = NullValueHandling.Ignore)]
        public long? SizeBytes { get; set; }
    }
}
