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
        [YamlMember(Alias = "sslEnabled")]
        [JsonProperty("sslEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SslEnabled { get; set; }

        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     Indicates whether the storage for a volume should be ThickProvisioned or ThinProvisioned.
        /// </summary>
        [YamlMember(Alias = "storageMode")]
        [JsonProperty("storageMode", NullValueHandling = NullValueHandling.Ignore)]
        public string StorageMode { get; set; }

        /// <summary>
        ///     The name of a volume already created in the ScaleIO system that is associated with this volume source.
        /// </summary>
        [YamlMember(Alias = "volumeName")]
        [JsonProperty("volumeName", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeName { get; set; }

        /// <summary>
        ///     SecretRef references to the secret for ScaleIO user and other sensitive information. If this is not provided, Login operation will fail.
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Include)]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     The ScaleIO Storage Pool associated with the protection domain.
        /// </summary>
        [YamlMember(Alias = "storagePool")]
        [JsonProperty("storagePool", NullValueHandling = NullValueHandling.Ignore)]
        public string StoragePool { get; set; }

        /// <summary>
        ///     The name of the storage system as configured in ScaleIO.
        /// </summary>
        [YamlMember(Alias = "system")]
        [JsonProperty("system", NullValueHandling = NullValueHandling.Include)]
        public string System { get; set; }

        /// <summary>
        ///     The name of the ScaleIO Protection Domain for the configured storage.
        /// </summary>
        [YamlMember(Alias = "protectionDomain")]
        [JsonProperty("protectionDomain", NullValueHandling = NullValueHandling.Ignore)]
        public string ProtectionDomain { get; set; }

        /// <summary>
        ///     The host address of the ScaleIO API Gateway.
        /// </summary>
        [YamlMember(Alias = "gateway")]
        [JsonProperty("gateway", NullValueHandling = NullValueHandling.Include)]
        public string Gateway { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
