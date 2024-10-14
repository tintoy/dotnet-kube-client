using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a source location of a volume to mount, managed by an external CSI driver
    /// </summary>
    public partial class CSIVolumeSourceV1
    {
        /// <summary>
        ///     fsType to mount. Ex. "ext4", "xfs", "ntfs". If not provided, the empty value is passed to the associated CSI driver which will determine the default filesystem to apply.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     nodePublishSecretRef is a reference to the secret object containing sensitive information to pass to the CSI driver to complete the CSI NodePublishVolume and NodeUnpublishVolume calls. This field is optional, and  may be empty if no secret is required. If the secret object contains more than one secret, all secret references are passed.
        /// </summary>
        [YamlMember(Alias = "nodePublishSecretRef")]
        [JsonProperty("nodePublishSecretRef", NullValueHandling = NullValueHandling.Ignore)]
        public LocalObjectReferenceV1 NodePublishSecretRef { get; set; }

        /// <summary>
        ///     driver is the name of the CSI driver that handles this volume. Consult with your admin for the correct name as registered in the cluster.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     volumeAttributes stores driver-specific properties that are passed to the CSI driver. Consult your driver's documentation for supported values.
        /// </summary>
        [YamlMember(Alias = "volumeAttributes")]
        [JsonProperty("volumeAttributes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> VolumeAttributes { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeAttributes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeAttributes() => VolumeAttributes.Count > 0;

        /// <summary>
        ///     readOnly specifies a read-only configuration for the volume. Defaults to false (read/write).
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
