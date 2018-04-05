using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationControllerCondition describes the state of a replication controller at a certain point.
    /// </summary>
    public partial class ReplicationControllerConditionV1
    {
        /// <summary>
        ///     A human readable message indicating details about the transition.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     The last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     The reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///     Type of replication controller condition.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
