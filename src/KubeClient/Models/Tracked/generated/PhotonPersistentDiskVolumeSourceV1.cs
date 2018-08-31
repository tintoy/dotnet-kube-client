using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a Photon Controller persistent disk resource.
    /// </summary>
    public partial class PhotonPersistentDiskVolumeSourceV1 : Models.PhotonPersistentDiskVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     ID that identifies Photon Controller persistent disk
        /// </summary>
        [JsonProperty("pdID")]
        [YamlMember(Alias = "pdID")]
        public override string PdID
        {
            get
            {
                return base.PdID;
            }
            set
            {
                base.PdID = value;

                __ModifiedProperties__.Add("PdID");
            }
        }


        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public override string FsType
        {
            get
            {
                return base.FsType;
            }
            set
            {
                base.FsType = value;

                __ModifiedProperties__.Add("FsType");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
