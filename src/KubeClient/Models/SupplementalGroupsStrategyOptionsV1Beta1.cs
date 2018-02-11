using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SupplementalGroupsStrategyOptions defines the strategy type and options used to create the strategy.
    /// </summary>
    [KubeResource("SupplementalGroupsStrategyOptions", "v1beta1")]
    public class SupplementalGroupsStrategyOptionsV1Beta1
    {
        /// <summary>
        ///     Rule is the strategy that will dictate what supplemental groups is used in the SecurityContext.
        /// </summary>
        [JsonProperty("rule")]
        public string Rule { get; set; }

        /// <summary>
        ///     Ranges are the allowed ranges of supplemental groups.  If you would like to force a single supplemental group then supply a single range with the same start and end.
        /// </summary>
        [JsonProperty("ranges", NullValueHandling = NullValueHandling.Ignore)]
        public List<IDRangeV1Beta1> Ranges { get; set; } = new List<IDRangeV1Beta1>();
    }
}
