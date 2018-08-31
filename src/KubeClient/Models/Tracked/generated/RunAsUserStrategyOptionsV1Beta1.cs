using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Run A sUser Strategy Options defines the strategy type and any options used to create the strategy.
    /// </summary>
    public partial class RunAsUserStrategyOptionsV1Beta1 : Models.RunAsUserStrategyOptionsV1Beta1, ITracked
    {
        /// <summary>
        ///     Rule is the strategy that will dictate the allowable RunAsUser values that may be set.
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
        ///     Ranges are the allowed ranges of uids that may be used.
        /// </summary>
        [YamlMember(Alias = "ranges")]
        [JsonProperty("ranges", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.IDRangeV1Beta1> Ranges { get; set; } = new List<Models.IDRangeV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
