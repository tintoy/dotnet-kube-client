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
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     Optional: FC target lun number
        /// </summary>
        [YamlMember(Alias = "lun")]
        [JsonProperty("lun", NullValueHandling = NullValueHandling.Ignore)]
        public int? Lun { get; set; }

        /// <summary>
        ///     Optional: FC target worldwide names (WWNs)
        /// </summary>
        [YamlMember(Alias = "targetWWNs")]
        [JsonProperty("targetWWNs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> TargetWWNs { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="TargetWWNs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTargetWWNs() => TargetWWNs.Count > 0;

        /// <summary>
        ///     Optional: FC volume world wide identifiers (wwids) Either wwids or combination of targetWWNs and lun must be set, but not both simultaneously.
        /// </summary>
        [YamlMember(Alias = "wwids")]
        [JsonProperty("wwids", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Wwids { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Wwids"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeWwids() => Wwids.Count > 0;

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
