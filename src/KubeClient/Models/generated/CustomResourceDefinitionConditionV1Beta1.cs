using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionCondition contains details for the current condition of this pod.
    /// </summary>
    public partial class CustomResourceDefinitionConditionV1Beta1
    {
        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        [YamlMember(Alias = "lastTransitionTime")]
        public virtual DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human-readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public virtual string Message { get; set; }

        /// <summary>
        ///     Type is the type of the condition.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }

        /// <summary>
        ///     Unique, one-word, CamelCase reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public virtual string Reason { get; set; }

        /// <summary>
        ///     Status is the status of the condition. Can be True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public virtual string Status { get; set; }
    }
}
