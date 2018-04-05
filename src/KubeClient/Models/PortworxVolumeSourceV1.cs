using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PortworxVolumeSource represents a Portworx volume resource.
    /// </summary>
    public partial class PortworxVolumeSourceV1
    {
        /// <summary>
        ///     VolumeID uniquely identifies a Portworx volume
        /// </summary>
        [JsonProperty("volumeID")]
        public string VolumeID { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        public bool ReadOnly { get; set; }

        /// <summary>
        ///     FSType represents the filesystem type to mount Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        public string FsType { get; set; }
    }
}
