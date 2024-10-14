using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowSchemaCondition describes conditions for a FlowSchema.
    /// </summary>
    public partial class FlowSchemaConditionV1Beta3
    {
        /// <summary>
        ///     `lastTransitionTime` is the last time the condition transitioned from one status to another.
        /// </summary>
        [YamlMember(Alias = "lastTransitionTime")]
        [JsonProperty("lastTransitionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     `message` is a human-readable message indicating details about last transition.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     `type` is the type of the condition. Required.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     `reason` is a unique, one-word, CamelCase reason for the condition's last transition.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     `status` is the status of the condition. Can be True, False, Unknown. Required.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
