using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Photon Controller persistent disk resource.
    /// </summary>
    [KubeResource("PhotonPersistentDiskVolumeSource", "v1")]
    public class PhotonPersistentDiskVolumeSourceV1
    {
        /// <summary>
        ///     ID that identifies Photon Controller persistent disk
        /// </summary>
        [JsonProperty("pdID")]
        public string PdID { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        public string FsType { get; set; }
    }
}
