using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a host path mapped into a pod. Host path volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    [KubeResource("HostPathVolumeSource", "v1")]
    public class HostPathVolumeSourceV1
    {
        /// <summary>
        ///     Path of the directory on the host. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
