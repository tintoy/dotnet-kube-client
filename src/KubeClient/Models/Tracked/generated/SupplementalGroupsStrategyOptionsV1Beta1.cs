using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SupplementalGroupsStrategyOptions defines the strategy type and options used to create the strategy.
    /// </summary>
    public partial class SupplementalGroupsStrategyOptionsV1Beta1 : Models.SupplementalGroupsStrategyOptionsV1Beta1, ITracked
    {
        /// <summary>
        ///     Rule is the strategy that will dictate what supplemental groups is used in the SecurityContext.
        /// </summary>
        [JsonProperty("rule")]
        [YamlMember(Alias = "rule")]
        public override string Rule
        {
            get
            {
                return base.Rule;
            }
            set
            {
                base.Rule = value;

                __ModifiedProperties__.Add("Rule");
            }
        }


        /// <summary>
        ///     Ranges are the allowed ranges of supplemental groups.  If you would like to force a single supplemental group then supply a single range with the same start and end.
        /// </summary>
        [YamlMember(Alias = "ranges")]
        [JsonProperty("ranges", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.IDRangeV1Beta1> Ranges { get; set; } = new List<Models.IDRangeV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
