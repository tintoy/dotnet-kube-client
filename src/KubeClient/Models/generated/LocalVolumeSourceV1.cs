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
        ///     fsType is the filesystem type to mount. It applies only when the Path is a block device. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". The default value is to auto-select a filesystem if unspecified.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     path of the full path to the volume on the node. It can be either a directory or block device (disk, partition, ...).
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }
    }
}
