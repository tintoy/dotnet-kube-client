using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentCondition describes the state of a deployment at a certain point.
    /// </summary>
    public partial class DeploymentConditionV1Beta1
    {
        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     A human readable message indicating details about the transition.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///     The reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     The last time this condition was updated.
        /// </summary>
        [JsonProperty("lastUpdateTime")]
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     Type of deployment condition.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
