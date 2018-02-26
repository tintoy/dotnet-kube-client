using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimSpec describes the common attributes of storage devices and allows a Source for provider-specific attributes
    /// </summary>
    [KubeObject("PersistentVolumeClaimSpec", "v1")]
    public class PersistentVolumeClaimSpecV1
    {
        /// <summary>
        ///     Name of the StorageClass required by the claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#class-1
        /// </summary>
        [JsonProperty("storageClassName")]
        public string StorageClassName { get; set; }

        /// <summary>
        ///     VolumeName is the binding reference to the PersistentVolume backing this claim.
        /// </summary>
        [JsonProperty("volumeName")]
        public string VolumeName { get; set; }

        /// <summary>
        ///     A label query over volumes to consider for binding.
        /// </summary>
        [JsonProperty("selector")]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     AccessModes contains the desired access modes the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes-1
        /// </summary>
        [JsonProperty("accessModes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AccessModes { get; set; } = new List<string>();

        /// <summary>
        ///     Resources represents the minimum resources the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#resources
        /// </summary>
        [JsonProperty("resources")]
        public ResourceRequirementsV1 Resources { get; set; }
    }
}
