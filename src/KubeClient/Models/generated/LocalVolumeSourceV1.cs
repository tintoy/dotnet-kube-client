using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Local represents directly-attached storage with node affinity (Beta feature)
    /// </summary>
    public partial class LocalVolumeSourceV1
    {
        /// <summary>
        ///     The full path to the volume on the node. It can be either a directory or block device (disk, partition, ...). Directories can be represented only by PersistentVolume with VolumeMode=Filesystem. Block devices can be represented only by VolumeMode=Block, which also requires the BlockVolume alpha feature gate to be enabled.
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public string Path { get; set; }
    }
}
