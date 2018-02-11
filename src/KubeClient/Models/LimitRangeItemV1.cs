using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     LimitRangeItem defines a min/max usage limit for any resource that matches on kind.
    /// </summary>
    [KubeResource("LimitRangeItem", "v1")]
    public class LimitRangeItemV1
    {
        /// <summary>
        ///     Type of resource that this limit applies to.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Min usage constraints on this kind by resource name.
        /// </summary>
        [JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Min { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     MaxLimitRequestRatio if specified, the named resource must have a request and limit that are both non-zero where limit divided by request is less than or equal to the enumerated value; this represents the max burst for the named resource.
        /// </summary>
        [JsonProperty("maxLimitRequestRatio", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> MaxLimitRequestRatio { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Default resource requirement limit value by resource name if resource limit is omitted.
        /// </summary>
        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Default { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     DefaultRequest is the default resource requirement request value by resource name if resource request is omitted.
        /// </summary>
        [JsonProperty("defaultRequest", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> DefaultRequest { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Max usage constraints on this kind by resource name.
        /// </summary>
        [JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Max { get; set; } = new Dictionary<string, string>();
    }
}
