using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a Fibre Channel volume. Fibre Channel volumes can only be mounted as read/write once. Fibre Channel volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class FCVolumeSourceV1 : Models.FCVolumeSourceV1, ITracked
    {
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
        ///     Required: FC target lun number
        /// </summary>
        [JsonProperty("lun")]
        [YamlMember(Alias = "lun")]
        public override int Lun
        {
            get
            {
                return base.Lun;
            }
            set
            {
                base.Lun = value;

                __ModifiedProperties__.Add("Lun");
            }
        }


        /// <summary>
        ///     Required: FC target worldwide names (WWNs)
        /// </summary>
        [YamlMember(Alias = "targetWWNs")]
        [JsonProperty("targetWWNs", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> TargetWWNs { get; set; } = new List<string>();

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
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
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
