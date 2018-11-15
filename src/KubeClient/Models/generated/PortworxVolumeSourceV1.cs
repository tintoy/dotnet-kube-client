using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

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
        [YamlMember(Alias = "volumeID")]
        [JsonProperty("volumeID", NullValueHandling = NullValueHandling.Include)]
        public string VolumeID { get; set; }

        /// <summary>
        ///     FSType represents the filesystem type to mount Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
