using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Describe a container image
    /// </summary>
    [KubeObject("ContainerImage", "v1")]
    public class ContainerImageV1
    {
        /// <summary>
        ///     Names by which this image is known. e.g. ["gcr.io/google_containers/hyperkube:v1.0.7", "dockerhub.io/google_containers/hyperkube:v1.0.7"]
        /// </summary>
        [JsonProperty("names", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Names { get; set; } = new List<string>();

        /// <summary>
        ///     The size of the image in bytes.
        /// </summary>
        [JsonProperty("sizeBytes")]
        public int SizeBytes { get; set; }
    }
}
