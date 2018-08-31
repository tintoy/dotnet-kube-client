using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a Persistent Disk resource in Google Compute Engine.
    ///     
    ///     A GCE PD must exist before mounting to a container. The disk must also be in the same GCE project and zone as the kubelet. A GCE PD can only be mounted as read/write once or read-only many times. GCE PDs support ownership management and SELinux relabeling.
    /// </summary>
    public partial class GCEPersistentDiskVolumeSourceV1 : Models.GCEPersistentDiskVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
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
        ///     Unique name of the PD resource in GCE. Used to identify the disk in GCE. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [JsonProperty("pdName")]
        [YamlMember(Alias = "pdName")]
        public override string PdName
        {
            get
            {
                return base.PdName;
            }
            set
            {
                base.PdName = value;

                __ModifiedProperties__.Add("PdName");
            }
        }


        /// <summary>
        ///     The partition in the volume that you want to mount. If omitted, the default is to mount by volume name. Examples: For volume /dev/sda1, you specify the partition as "1". Similarly, the volume partition for /dev/sda is "0" (or you can leave the property empty). More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
        /// </summary>
        [JsonProperty("partition")]
        [YamlMember(Alias = "partition")]
        public override int Partition
        {
            get
            {
                return base.Partition;
            }
            set
            {
                base.Partition = value;

                __ModifiedProperties__.Add("Partition");
            }
        }


        /// <summary>
        ///     ReadOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false. More info: https://kubernetes.io/docs/concepts/storage/volumes#gcepersistentdisk
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
