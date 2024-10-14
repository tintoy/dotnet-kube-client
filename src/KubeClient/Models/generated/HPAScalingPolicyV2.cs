using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HPAScalingPolicy is a single policy which must hold true for a specified past interval.
    /// </summary>
    public partial class HPAScalingPolicyV2
    {
        /// <summary>
        ///     type is used to specify the scaling policy.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     value contains the amount of change which is permitted by the policy. It must be greater than zero
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Include)]
        public int Value { get; set; }

        /// <summary>
        ///     periodSeconds specifies the window of time for which the policy should hold true. PeriodSeconds must be greater than zero and less than or equal to 1800 (30 min).
        /// </summary>
        [YamlMember(Alias = "periodSeconds")]
        [JsonProperty("periodSeconds", NullValueHandling = NullValueHandling.Include)]
        public int PeriodSeconds { get; set; }
    }
}
