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
        ///     Storage Policy Based Management (SPBM) profile ID associated with the StoragePolicyName.
        /// </summary>
        [YamlMember(Alias = "storagePolicyID")]
        [JsonProperty("storagePolicyID", NullValueHandling = NullValueHandling.Ignore)]
        public string StoragePolicyID { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     Storage Policy Based Management (SPBM) profile name.
        /// </summary>
        [YamlMember(Alias = "storagePolicyName")]
        [JsonProperty("storagePolicyName", NullValueHandling = NullValueHandling.Ignore)]
        public string StoragePolicyName { get; set; }

        /// <summary>
        ///     Path that identifies vSphere volume vmdk
        /// </summary>
        [YamlMember(Alias = "volumePath")]
        [JsonProperty("volumePath", NullValueHandling = NullValueHandling.Include)]
        public string VolumePath { get; set; }
    }
}
