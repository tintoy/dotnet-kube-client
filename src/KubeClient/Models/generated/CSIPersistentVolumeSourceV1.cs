using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents storage that is managed by an external CSI volume driver (Beta feature)
    /// </summary>
    public partial class CSIPersistentVolumeSourceV1
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs".
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     VolumeHandle is the unique volume name returned by the CSI volume plugin’s CreateVolume to refer to the volume on all subsequent calls. Required.
        /// </summary>
        [YamlMember(Alias = "volumeHandle")]
        [JsonProperty("volumeHandle", NullValueHandling = NullValueHandling.Include)]
        public string VolumeHandle { get; set; }

        /// <summary>
        ///     ControllerPublishSecretRef is a reference to the secret object containing sensitive information to pass to the CSI driver to complete the CSI ControllerPublishVolume and ControllerUnpublishVolume calls. This field is optional, and  may be empty if no secret is required. If the secret object contains more than one secret, all secrets are passed.
        /// </summary>
        [YamlMember(Alias = "controllerPublishSecretRef")]
        [JsonProperty("controllerPublishSecretRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretReferenceV1 ControllerPublishSecretRef { get; set; }

        /// <summary>
        ///     NodePublishSecretRef is a reference to the secret object containing sensitive information to pass to the CSI driver to complete the CSI NodePublishVolume and NodeUnpublishVolume calls. This field is optional, and  may be empty if no secret is required. If the secret object contains more than one secret, all secrets are passed.
        /// </summary>
        [YamlMember(Alias = "nodePublishSecretRef")]
        [JsonProperty("nodePublishSecretRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretReferenceV1 NodePublishSecretRef { get; set; }

        /// <summary>
        ///     NodeStageSecretRef is a reference to the secret object containing sensitive information to pass to the CSI driver to complete the CSI NodeStageVolume and NodeStageVolume and NodeUnstageVolume calls. This field is optional, and  may be empty if no secret is required. If the secret object contains more than one secret, all secrets are passed.
        /// </summary>
        [YamlMember(Alias = "nodeStageSecretRef")]
        [JsonProperty("nodeStageSecretRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretReferenceV1 NodeStageSecretRef { get; set; }

        /// <summary>
        ///     Driver is the name of the driver to use for this volume. Required.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     Attributes of the volume to publish.
        /// </summary>
        [YamlMember(Alias = "volumeAttributes")]
        [JsonProperty("volumeAttributes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> VolumeAttributes { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeAttributes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeAttributes() => VolumeAttributes.Count > 0;

        /// <summary>
        ///     Optional: The value to pass to ControllerPublishVolumeRequest. Defaults to false (read/write).
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
