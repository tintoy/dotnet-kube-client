using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Information about the condition of a component.
    /// </summary>
    public partial class ComponentConditionV1
    {
        /// <summary>
        ///     Message about the condition for a component. For example, information about a health check.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     Type of condition for a component. Valid value: "Healthy"
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     Condition error code for a component. For example, a health check error code.
        /// </summary>
        [YamlMember(Alias = "error")]
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        ///     Status of the condition for a component. Valid values for "Healthy": "True", "False", or "Unknown".
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
