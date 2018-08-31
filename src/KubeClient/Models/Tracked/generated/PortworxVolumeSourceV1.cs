using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PortworxVolumeSource represents a Portworx volume resource.
    /// </summary>
    public partial class PortworxVolumeSourceV1 : Models.PortworxVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     VolumeID uniquely identifies a Portworx volume
        /// </summary>
        [JsonProperty("volumeID")]
        [YamlMember(Alias = "volumeID")]
        public override string VolumeID
        {
            get
            {
                return base.VolumeID;
            }
            set
            {
                base.VolumeID = value;

                __ModifiedProperties__.Add("VolumeID");
            }
        }


        /// <summary>
        ///     FSType represents the filesystem type to mount Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs". Implicitly inferred to be "ext4" if unspecified.
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
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                __ModifiedProperties__.Add("ReadOnly");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
