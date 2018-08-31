using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeClaimSpec describes the common attributes of storage devices and allows a Source for provider-specific attributes
    /// </summary>
    public partial class PersistentVolumeClaimSpecV1 : Models.PersistentVolumeClaimSpecV1, ITracked
    {
        /// <summary>
        ///     Name of the StorageClass required by the claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#class-1
        /// </summary>
        [JsonProperty("storageClassName")]
        [YamlMember(Alias = "storageClassName")]
        public override string StorageClassName
        {
            get
            {
                return base.StorageClassName;
            }
            set
            {
                base.StorageClassName = value;

                __ModifiedProperties__.Add("StorageClassName");
            }
        }


        /// <summary>
        ///     VolumeName is the binding reference to the PersistentVolume backing this claim.
        /// </summary>
        [JsonProperty("volumeName")]
        [YamlMember(Alias = "volumeName")]
        public override string VolumeName
        {
            get
            {
                return base.VolumeName;
            }
            set
            {
                base.VolumeName = value;

                __ModifiedProperties__.Add("VolumeName");
            }
        }


        /// <summary>
        ///     A label query over volumes to consider for binding.
        /// </summary>
        [JsonProperty("selector")]
        [YamlMember(Alias = "selector")]
        public override Models.LabelSelectorV1 Selector
        {
            get
            {
                return base.Selector;
            }
            set
            {
                base.Selector = value;

                __ModifiedProperties__.Add("Selector");
            }
        }


        /// <summary>
        ///     AccessModes contains the desired access modes the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes-1
        /// </summary>
        [YamlMember(Alias = "accessModes")]
        [JsonProperty("accessModes", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> AccessModes { get; set; } = new List<string>();

        /// <summary>
        ///     Resources represents the minimum resources the volume should have. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#resources
        /// </summary>
        [JsonProperty("resources")]
        [YamlMember(Alias = "resources")]
        public override Models.ResourceRequirementsV1 Resources
        {
            get
            {
                return base.Resources;
            }
            set
            {
                base.Resources = value;

                __ModifiedProperties__.Add("Resources");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
