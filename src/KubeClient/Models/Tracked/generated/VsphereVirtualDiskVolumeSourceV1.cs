using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a vSphere volume resource.
    /// </summary>
    public partial class VsphereVirtualDiskVolumeSourceV1 : Models.VsphereVirtualDiskVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Storage Policy Based Management (SPBM) profile ID associated with the StoragePolicyName.
        /// </summary>
        [JsonProperty("storagePolicyID")]
        [YamlMember(Alias = "storagePolicyID")]
        public override string StoragePolicyID
        {
            get
            {
                return base.StoragePolicyID;
            }
            set
            {
                base.StoragePolicyID = value;

                __ModifiedProperties__.Add("StoragePolicyID");
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
        ///     Storage Policy Based Management (SPBM) profile name.
        /// </summary>
        [JsonProperty("storagePolicyName")]
        [YamlMember(Alias = "storagePolicyName")]
        public override string StoragePolicyName
        {
            get
            {
                return base.StoragePolicyName;
            }
            set
            {
                base.StoragePolicyName = value;

                __ModifiedProperties__.Add("StoragePolicyName");
            }
        }


        /// <summary>
        ///     Path that identifies vSphere volume vmdk
        /// </summary>
        [JsonProperty("volumePath")]
        [YamlMember(Alias = "volumePath")]
        public override string VolumePath
        {
            get
            {
                return base.VolumePath;
            }
            set
            {
                base.VolumePath = value;

                __ModifiedProperties__.Add("VolumePath");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
