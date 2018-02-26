using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Information about the condition of a component.
    /// </summary>
    [KubeObject("ComponentCondition", "v1")]
    public class ComponentConditionV1
    {
        /// <summary>
        ///     Message about the condition for a component. For example, information about a health check.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Type of condition for a component. Valid value: "Healthy"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Condition error code for a component. For example, a health check error code.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        ///     Status of the condition for a component. Valid values for "Healthy": "True", "False", or "Unknown".
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
