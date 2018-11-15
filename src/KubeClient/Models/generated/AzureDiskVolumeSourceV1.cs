using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AzureDisk represents an Azure Data Disk mount on the host and bind mount to the pod.
    /// </summary>
    public partial class AzureDiskVolumeSourceV1
    {
        /// <summary>
        ///     The URI the data disk in the blob storage
        /// </summary>
        [YamlMember(Alias = "diskURI")]
        [JsonProperty("diskURI", NullValueHandling = NullValueHandling.Include)]
        public string DiskURI { get; set; }

        /// <summary>
        ///     Expected values Shared: multiple blob disks per storage account  Dedicated: single blob disk per storage account  Managed: azure managed data disk (only in managed availability set). defaults to shared
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        /// <summary>
        ///     Host Caching mode: None, Read Only, Read Write.
        /// </summary>
        [YamlMember(Alias = "cachingMode")]
        [JsonProperty("cachingMode", NullValueHandling = NullValueHandling.Ignore)]
        public string CachingMode { get; set; }

        /// <summary>
        ///     The Name of the data disk in the blob storage
        /// </summary>
        [YamlMember(Alias = "diskName")]
        [JsonProperty("diskName", NullValueHandling = NullValueHandling.Include)]
        public string DiskName { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
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
