using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SupplementalGroupsStrategyOptions defines the strategy type and options used to create the strategy. Deprecated: use SupplementalGroupsStrategyOptions from policy API Group instead.
    /// </summary>
    public partial class SupplementalGroupsStrategyOptionsV1Beta1
    {
        /// <summary>
        ///     rule is the strategy that will dictate what supplemental groups is used in the SecurityContext.
        /// </summary>
        [YamlMember(Alias = "rule")]
        [JsonProperty("rule", NullValueHandling = NullValueHandling.Ignore)]
        public string Rule { get; set; }

        /// <summary>
        ///     ranges are the allowed ranges of supplemental groups.  If you would like to force a single supplemental group then supply a single range with the same start and end. Required for MustRunAs.
        /// </summary>
        [YamlMember(Alias = "ranges")]
        [JsonProperty("ranges", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IDRangeV1Beta1> Ranges { get; } = new List<IDRangeV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Ranges"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRanges() => Ranges.Count > 0;
    }
}
