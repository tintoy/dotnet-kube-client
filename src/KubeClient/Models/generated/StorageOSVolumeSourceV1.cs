using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a StorageOS persistent volume resource.
    /// </summary>
    public partial class StorageOSVolumeSourceV1
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     VolumeName is the human-readable name of the StorageOS volume.  Volume names are only unique within a namespace.
        /// </summary>
        [YamlMember(Alias = "volumeName")]
        [JsonProperty("volumeName", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeName { get; set; }

        /// <summary>
        ///     VolumeNamespace specifies the scope of the volume within StorageOS.  If no namespace is specified then the Pod's namespace will be used.  This allows the Kubernetes name scoping to be mirrored within StorageOS for tighter integration. Set VolumeName to any name to override the default behaviour. Set to "default" if you are not using namespaces within StorageOS. Namespaces that do not pre-exist within StorageOS will be created.
        /// </summary>
        [YamlMember(Alias = "volumeNamespace")]
        [JsonProperty("volumeNamespace", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeNamespace { get; set; }

        /// <summary>
        ///     SecretRef specifies the secret to use for obtaining the StorageOS API credentials.  If not specified, default values will be attempted.
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
