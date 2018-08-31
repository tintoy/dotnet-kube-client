using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeClaimStatus is the current status of a persistent volume claim.
    /// </summary>
    public partial class PersistentVolumeClaimStatusV1 : Models.PersistentVolumeClaimStatusV1, ITracked
    {
        /// <summary>
        ///     Phase represents the current phase of PersistentVolumeClaim.
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public override string Phase
        {
            get
            {
                return base.Phase;
            }
            set
            {
                base.Phase = value;

                __ModifiedProperties__.Add("Phase");
            }
        }


        /// <summary>
        ///     AccessModes contains the actual access modes the volume backing the PVC has. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes-1
        /// </summary>
        [YamlMember(Alias = "accessModes")]
        [JsonProperty("accessModes", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> AccessModes { get; set; } = new List<string>();

        /// <summary>
        ///     Represents the actual resources of the underlying volume.
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Capacity { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
