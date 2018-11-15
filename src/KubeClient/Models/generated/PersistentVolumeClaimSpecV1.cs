using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimSpec describes the common attributes of storage devices and allows a Source for provider-specific attributes
    /// </summary>
    public partial class PersistentVolumeClaimSpecV1
    {
        /// <summary>
        ///     Name of the StorageClass required by the claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#class-1
        /// </summary>
        [YamlMember(Alias = "storageClassName")]
        [JsonProperty("storageClassName", NullValueHandling = NullValueHandling.Ignore)]
        public string StorageClassName { get; set; }

        /// <summary>
        ///     volumeMode defines what type of volume is required by the claim. Value of Filesystem is implied when not included in claim spec. This is an alpha feature and may change in the future.
        /// </summary>
        [YamlMember(Alias = "volumeMode")]
        [JsonProperty("volumeMode", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeMode { get; set; }

        /// <summary>
        ///     VolumeName is the binding reference to the PersistentVolume backing this claim.
        /// </summary>
        [YamlMember(Alias = "volumeName")]
        [JsonProperty("volumeName", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeName { get; set; }

        /// <summary>
        ///     A label query over volumes to consider for binding.
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     AccessModes contains the desired access modes the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes-1
        /// </summary>
        [YamlMember(Alias = "accessModes")]
        [JsonProperty("accessModes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> AccessModes { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="AccessModes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAccessModes() => AccessModes.Count > 0;

        /// <summary>
        ///     Resources represents the minimum resources the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#resources
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceRequirementsV1 Resources { get; set; }
    }
}
