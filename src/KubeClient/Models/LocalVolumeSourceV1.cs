using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Local represents directly-attached storage with node affinity
    /// </summary>
    [KubeResource("LocalVolumeSource", "v1")]
    public class LocalVolumeSourceV1
    {
        /// <summary>
        ///     The full path to the volume on the node For alpha, this path must be a directory Once block as a source is supported, then this path can point to a block device
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
