using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     LimitRangeItem defines a min/max usage limit for any resource that matches on kind.
    /// </summary>
    public partial class LimitRangeItemV1 : Models.LimitRangeItemV1, ITracked
    {
        /// <summary>
        ///     Type of resource that this limit applies to.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     Min usage constraints on this kind by resource name.
        /// </summary>
        [YamlMember(Alias = "min")]
        [JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Min { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     MaxLimitRequestRatio if specified, the named resource must have a request and limit that are both non-zero where limit divided by request is less than or equal to the enumerated value; this represents the max burst for the named resource.
        /// </summary>
        [YamlMember(Alias = "maxLimitRequestRatio")]
        [JsonProperty("maxLimitRequestRatio", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> MaxLimitRequestRatio { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Default resource requirement limit value by resource name if resource limit is omitted.
        /// </summary>
        [YamlMember(Alias = "default")]
        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Default { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     DefaultRequest is the default resource requirement request value by resource name if resource request is omitted.
        /// </summary>
        [YamlMember(Alias = "defaultRequest")]
        [JsonProperty("defaultRequest", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> DefaultRequest { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Max usage constraints on this kind by resource name.
        /// </summary>
        [YamlMember(Alias = "max")]
        [JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Max { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
