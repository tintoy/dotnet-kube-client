using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LimitRangeItem defines a min/max usage limit for any resource that matches on kind.
    /// </summary>
    public partial class LimitRangeItemV1
    {
        /// <summary>
        ///     Type of resource that this limit applies to.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     Min usage constraints on this kind by resource name.
        /// </summary>
        [YamlMember(Alias = "min")]
        [JsonProperty("min", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Min { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Min"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMin() => Min.Count > 0;

        /// <summary>
        ///     MaxLimitRequestRatio if specified, the named resource must have a request and limit that are both non-zero where limit divided by request is less than or equal to the enumerated value; this represents the max burst for the named resource.
        /// </summary>
        [YamlMember(Alias = "maxLimitRequestRatio")]
        [JsonProperty("maxLimitRequestRatio", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> MaxLimitRequestRatio { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="MaxLimitRequestRatio"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMaxLimitRequestRatio() => MaxLimitRequestRatio.Count > 0;

        /// <summary>
        ///     Default resource requirement limit value by resource name if resource limit is omitted.
        /// </summary>
        [YamlMember(Alias = "default")]
        [JsonProperty("default", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Default { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Default"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDefault() => Default.Count > 0;

        /// <summary>
        ///     DefaultRequest is the default resource requirement request value by resource name if resource request is omitted.
        /// </summary>
        [YamlMember(Alias = "defaultRequest")]
        [JsonProperty("defaultRequest", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> DefaultRequest { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="DefaultRequest"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDefaultRequest() => DefaultRequest.Count > 0;

        /// <summary>
        ///     Max usage constraints on this kind by resource name.
        /// </summary>
        [YamlMember(Alias = "max")]
        [JsonProperty("max", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Max { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Max"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMax() => Max.Count > 0;
    }
}
