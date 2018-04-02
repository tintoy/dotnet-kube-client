using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeCondition contains condition information for a node.
    /// </summary>
    [KubeObject("NodeCondition", "v1")]
    public partial class NodeConditionV1
    {
        /// <summary>
        ///     Last time we got an update on a given condition.
        /// </summary>
        [JsonProperty("lastHeartbeatTime")]
        public DateTime? LastHeartbeatTime { get; set; }

        /// <summary>
        ///     Last time the condition transit from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Type of node condition.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     (brief) reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
