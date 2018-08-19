using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ScaleIOVolumeSource represents a persistent ScaleIO volume
    /// </summary>
    public partial class ScaleIOVolumeSourceV1
    {
        /// <summary>
        ///     Flag to enable/disable SSL communication with Gateway, default false
        /// </summary>
        [JsonProperty("sslEnabled")]
        [YamlMember(Alias = "sslEnabled")]
        public bool SslEnabled { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public string FsType { get; set; }

        /// <summary>
        ///     Indicates whether the storage for a volume should be thick or thin (defaults to "thin").
        /// </summary>
        [JsonProperty("storageMode")]
        [YamlMember(Alias = "storageMode")]
        public string StorageMode { get; set; }

        /// <summary>
        ///     The name of a volume already created in the ScaleIO system that is associated with this volume source.
        /// </summary>
        [JsonProperty("volumeName")]
        [YamlMember(Alias = "volumeName")]
        public string VolumeName { get; set; }

        /// <summary>
        ///     SecretRef references to the secret for ScaleIO user and other sensitive information. If this is not provided, Login operation will fail.
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     The Storage Pool associated with the protection domain (defaults to "default").
        /// </summary>
        [JsonProperty("storagePool")]
        [YamlMember(Alias = "storagePool")]
        public string StoragePool { get; set; }

        /// <summary>
        ///     The name of the storage system as configured in ScaleIO.
        /// </summary>
        [JsonProperty("system")]
        [YamlMember(Alias = "system")]
        public string System { get; set; }

        /// <summary>
        ///     The name of the Protection Domain for the configured storage (defaults to "default").
        /// </summary>
        [JsonProperty("protectionDomain")]
        [YamlMember(Alias = "protectionDomain")]
        public string ProtectionDomain { get; set; }

        /// <summary>
        ///     The host address of the ScaleIO API Gateway.
        /// </summary>
        [JsonProperty("gateway")]
        [YamlMember(Alias = "gateway")]
        public string Gateway { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public bool ReadOnly { get; set; }
    }
}
