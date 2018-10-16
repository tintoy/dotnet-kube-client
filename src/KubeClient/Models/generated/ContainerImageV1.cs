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
        [JsonProperty("names", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Names { get; set; } = new List<string>();

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        [JsonProperty("sizeBytes")]
        [YamlMember(Alias = "sizeBytes")]
        public int SizeBytes { get; set; }
    }
}
