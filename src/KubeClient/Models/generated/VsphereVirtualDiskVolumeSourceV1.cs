using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a vSphere volume resource.
    /// </summary>
    public partial class VsphereVirtualDiskVolumeSourceV1
    {
        /// <summary>
        ///     Storage Policy Based Management (SPBM) profile name.
        /// </summary>
        [JsonProperty("storagePolicyName")]
        [YamlMember(Alias = "storagePolicyName")]
        public string StoragePolicyName { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public string FsType { get; set; }

        /// <summary>
        ///     Path that identifies vSphere volume vmdk
        /// </summary>
        [JsonProperty("volumePath")]
        [YamlMember(Alias = "volumePath")]
        public string VolumePath { get; set; }

        /// <summary>
        ///     Storage Policy Based Management (SPBM) profile ID associated with the StoragePolicyName.
        /// </summary>
        [JsonProperty("storagePolicyID")]
        [YamlMember(Alias = "storagePolicyID")]
        public string StoragePolicyID { get; set; }
    }
}
