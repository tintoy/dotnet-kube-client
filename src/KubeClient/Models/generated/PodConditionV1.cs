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
        ///     Type is the type of the condition. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     Unique, one-word, CamelCase reason for the condition's last transition.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     Status is the status of the condition. Can be True, False, Unknown. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
