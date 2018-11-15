using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimCondition contails details about state of pvc
    /// </summary>
    public partial class PersistentVolumeClaimConditionV1
    {
        /// <summary>
        ///     Last time we probed the condition.
        /// </summary>
        [YamlMember(Alias = "lastProbeTime")]
        [JsonProperty("lastProbeTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastProbeTime { get; set; }

        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [YamlMember(Alias = "lastTransitionTime")]
        [JsonProperty("lastTransitionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human-readable message indicating details about last transition.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     Unique, this should be a short, machine understandable string that gives the reason for condition's last transition. If it reports "ResizeStarted" that means the underlying persistent volume is being resized.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
