using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ScaleIOPersistentVolumeSource represents a persistent ScaleIO volume
    /// </summary>
    public partial class ScaleIOPersistentVolumeSourceV1
    {
        /// <summary>
        ///     sslEnabled is the flag to enable/disable SSL communication with Gateway, default false
        /// </summary>
        [YamlMember(Alias = "sslEnabled")]
        [JsonProperty("sslEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SslEnabled { get; set; }

        /// <summary>
        ///     fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Default is "xfs"
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     storageMode indicates whether the storage for a volume should be ThickProvisioned or ThinProvisioned. Default is ThinProvisioned.
        /// </summary>
        [YamlMember(Alias = "storageMode")]
        [JsonProperty("storageMode", NullValueHandling = NullValueHandling.Ignore)]
        public string StorageMode { get; set; }

        /// <summary>
        ///     volumeName is the name of a volume already created in the ScaleIO system that is associated with this volume source.
        /// </summary>
        [YamlMember(Alias = "volumeName")]
        [JsonProperty("volumeName", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeName { get; set; }

        /// <summary>
        ///     secretRef references to the secret for ScaleIO user and other sensitive information. If this is not provided, Login operation will fail.
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Include)]
        public SecretReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     storagePool is the ScaleIO Storage Pool associated with the protection domain.
        /// </summary>
        [YamlMember(Alias = "storagePool")]
        [JsonProperty("storagePool", NullValueHandling = NullValueHandling.Ignore)]
        public string StoragePool { get; set; }

        /// <summary>
        ///     system is the name of the storage system as configured in ScaleIO.
        /// </summary>
        [YamlMember(Alias = "system")]
        [JsonProperty("system", NullValueHandling = NullValueHandling.Include)]
        public string System { get; set; }

        /// <summary>
        ///     protectionDomain is the name of the ScaleIO Protection Domain for the configured storage.
        /// </summary>
        [YamlMember(Alias = "protectionDomain")]
        [JsonProperty("protectionDomain", NullValueHandling = NullValueHandling.Ignore)]
        public string ProtectionDomain { get; set; }

        /// <summary>
        ///     gateway is the host address of the ScaleIO API Gateway.
        /// </summary>
        [YamlMember(Alias = "gateway")]
        [JsonProperty("gateway", NullValueHandling = NullValueHandling.Include)]
        public string Gateway { get; set; }

        /// <summary>
        ///     readOnly defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
