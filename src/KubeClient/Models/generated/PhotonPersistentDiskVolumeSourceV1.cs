using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Photon Controller persistent disk resource.
    /// </summary>
    public partial class PhotonPersistentDiskVolumeSourceV1
    {
        /// <summary>
        ///     ID that identifies Photon Controller persistent disk
        /// </summary>
        [YamlMember(Alias = "pdID")]
        [JsonProperty("pdID", NullValueHandling = NullValueHandling.Include)]
        public string PdID { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }
    }
}
