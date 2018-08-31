using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
    /// </summary>
    public partial class AzureDiskVolumeSourceV1 : Models.AzureDiskVolumeSourceV1
    {
        /// <summary>
        ///     The URI the data disk in the blob storage
        /// </summary>
        [JsonProperty("diskURI")]
        [YamlMember(Alias = "diskURI")]
        public override string DiskURI
        {
            get
            {
                return base.DiskURI;
            }
            set
            {
                base.DiskURI = value;

                __ModifiedProperties__.Add("DiskURI");
            }
        }


        /// <summary>
        ///     Expected values Shared: mulitple blob disks per storage account  Dedicated: single blob disk per storage account  Managed: azure managed data disk (only in managed availability set). defaults to shared
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     Host Caching mode: None, Read Only, Read Write.
        /// </summary>
        [JsonProperty("cachingMode")]
        [YamlMember(Alias = "cachingMode")]
        public override string CachingMode
        {
            get
            {
                return base.CachingMode;
            }
            set
            {
                base.CachingMode = value;

                __ModifiedProperties__.Add("CachingMode");
            }
        }


        /// <summary>
        ///     The Name of the data disk in the blob storage
        /// </summary>
        [JsonProperty("diskName")]
        [YamlMember(Alias = "diskName")]
        public override string DiskName
        {
            get
            {
                return base.DiskName;
            }
            set
            {
                base.DiskName = value;

                __ModifiedProperties__.Add("DiskName");
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
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
