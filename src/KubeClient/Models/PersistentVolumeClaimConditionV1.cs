using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimCondition contails details about state of pvc
    /// </summary>
    [KubeObject("PersistentVolumeClaimCondition", "v1")]
    public partial class PersistentVolumeClaimConditionV1
    {
        /// <summary>
        ///     Last time we probed the condition.
        /// </summary>
        [JsonProperty("lastProbeTime")]
        public DateTime? LastProbeTime { get; set; }

        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human-readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Unique, this should be a short, machine understandable string that gives the reason for condition's last transition. If it reports "ResizeStarted" that means the underlying persistent volume is being resized.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
