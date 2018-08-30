using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimStatus is the current status of a persistent volume claim.
    /// </summary>
    public partial class PersistentVolumeClaimStatusV1
    {
        /// <summary>
        ///     Phase represents the current phase of PersistentVolumeClaim.
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public virtual string Phase { get; set; }

        /// <summary>
        ///     AccessModes contains the actual access modes the volume backing the PVC has. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#access-modes-1
        /// </summary>
        [YamlMember(Alias = "accessModes")]
        [JsonProperty("accessModes", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> AccessModes { get; set; } = new List<string>();

        /// <summary>
        ///     Represents the actual resources of the underlying volume.
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Dictionary<string, string> Capacity { get; set; } = new Dictionary<string, string>();
    }
}
