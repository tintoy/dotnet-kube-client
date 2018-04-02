using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionCondition contains details for the current condition of this pod.
    /// </summary>
    [KubeObject("CustomResourceDefinitionCondition", "v1beta1")]
    public partial class CustomResourceDefinitionConditionV1Beta1
    {
        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human-readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Type is the type of the condition.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Unique, one-word, CamelCase reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Status is the status of the condition. Can be True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
