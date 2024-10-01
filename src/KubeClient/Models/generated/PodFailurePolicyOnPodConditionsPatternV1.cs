using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodFailurePolicyOnPodConditionsPattern describes a pattern for matching an actual pod condition type.
    /// </summary>
    public partial class PodFailurePolicyOnPodConditionsPatternV1
    {
        /// <summary>
        ///     Specifies the required Pod condition type. To match a pod condition it is required that specified type equals the pod condition type.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     Specifies the required Pod condition status. To match a pod condition it is required that the specified status equals the pod condition status. Defaults to True.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
