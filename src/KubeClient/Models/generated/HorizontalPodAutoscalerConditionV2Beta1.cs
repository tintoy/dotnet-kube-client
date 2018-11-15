using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscalerCondition describes the state of a HorizontalPodAutoscaler at a certain point.
    /// </summary>
    public partial class HorizontalPodAutoscalerConditionV2Beta1
    {
        /// <summary>
        ///     lastTransitionTime is the last time the condition transitioned from one status to another
        /// </summary>
        [YamlMember(Alias = "lastTransitionTime")]
        [JsonProperty("lastTransitionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     message is a human-readable explanation containing details about the transition
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     type describes the current condition
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     reason is the reason for the condition's last transition.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     status is the status of the condition (True, False, Unknown)
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
