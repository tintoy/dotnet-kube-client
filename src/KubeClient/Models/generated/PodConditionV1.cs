using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodCondition contains details for the current condition of this pod.
    /// </summary>
    public partial class PodConditionV1
    {
        /// <summary>
        ///     Last time we probed the condition.
        /// </summary>
        [JsonProperty("lastProbeTime")]
        [YamlMember(Alias = "lastProbeTime")]
        public DateTime? LastProbeTime { get; set; }

        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        [YamlMember(Alias = "lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human-readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public string Message { get; set; }

        /// <summary>
        ///     Type is the type of the condition. Currently only Ready. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     Unique, one-word, CamelCase reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Status is the status of the condition. Can be True, False, Unknown. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public string Status { get; set; }
    }
}
