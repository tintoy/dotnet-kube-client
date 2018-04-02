using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a vSphere volume resource.
    /// </summary>
    [KubeObject("VsphereVirtualDiskVolumeSource", "v1")]
    public partial class VsphereVirtualDiskVolumeSourceV1
    {
        /// <summary>
        ///     Storage Policy Based Management (SPBM) profile ID associated with the StoragePolicyName.
        /// </summary>
        [JsonProperty("storagePolicyID")]
        public string StoragePolicyID { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        public string FsType { get; set; }

        /// <summary>
        ///     Storage Policy Based Management (SPBM) profile name.
        /// </summary>
        [JsonProperty("storagePolicyName")]
        public string StoragePolicyName { get; set; }

        /// <summary>
        ///     Path that identifies vSphere volume vmdk
        /// </summary>
        [JsonProperty("volumePath")]
        public string VolumePath { get; set; }
    }
}
