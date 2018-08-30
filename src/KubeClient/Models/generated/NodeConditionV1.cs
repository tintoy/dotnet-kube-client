using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeCondition contains condition information for a node.
    /// </summary>
    public partial class NodeConditionV1
    {
        /// <summary>
        ///     Last time we got an update on a given condition.
        /// </summary>
        [JsonProperty("lastHeartbeatTime")]
        [YamlMember(Alias = "lastHeartbeatTime")]
        public virtual DateTime? LastHeartbeatTime { get; set; }

        /// <summary>
        ///     Last time the condition transit from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        [YamlMember(Alias = "lastTransitionTime")]
        public virtual DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public virtual string Message { get; set; }

        /// <summary>
        ///     Type of node condition.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }

        /// <summary>
        ///     (brief) reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public virtual string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public virtual string Status { get; set; }
    }
}
