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
        [JsonProperty("lastTransitionTime")]
        [YamlMember(Alias = "lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     type describes the current condition
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     reason is the reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     status is the status of the condition (True, False, Unknown)
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public string Status { get; set; }

        /// <summary>
        ///     message is a human-readable explanation containing details about the transition
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public string Message { get; set; }
    }
}
