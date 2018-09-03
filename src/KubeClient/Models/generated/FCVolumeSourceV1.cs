using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Fibre Channel volume. Fibre Channel volumes can only be mounted as read/write once. Fibre Channel volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class FCVolumeSourceV1
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public string FsType { get; set; }

        /// <summary>
        ///     Optional: FC target worldwide names (WWNs)
        /// </summary>
        [YamlMember(Alias = "targetWWNs")]
        [JsonProperty("targetWWNs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TargetWWNs { get; set; } = new List<string>();

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public bool ReadOnly { get; set; }

        /// <summary>
        ///     Optional: FC target lun number
        /// </summary>
        [JsonProperty("lun")]
        [YamlMember(Alias = "lun")]
        public int? Lun { get; set; }

        /// <summary>
        ///     Optional: FC volume world wide identifiers (wwids) Either wwids or combination of targetWWNs and lun must be set, but not both simultaneously.
        /// </summary>
        [YamlMember(Alias = "wwids")]
        [JsonProperty("wwids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Wwids { get; set; } = new List<string>();
    }
}
