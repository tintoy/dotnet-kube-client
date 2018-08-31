using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a cinder volume resource in Openstack. A Cinder volume must exist before mounting to a container. The volume must also be in the same region as the kubelet. Cinder volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class CinderVolumeSourceV1 : Models.CinderVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     volume id used to identify the volume in cinder More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
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
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
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
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts. More info: https://releases.k8s.io/HEAD/examples/mysql-cinder-pd/README.md
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
