using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Run A sUser Strategy Options defines the strategy type and any options used to create the strategy.
    /// </summary>
    public partial class RunAsUserStrategyOptionsV1Beta1
    {
        /// <summary>
        ///     Rule is the strategy that will dictate the allowable RunAsUser values that may be set.
        /// </summary>
        [JsonProperty("rule")]
        [YamlMember(Alias = "rule")]
        public string Rule { get; set; }

        /// <summary>
        ///     Ranges are the allowed ranges of uids that may be used.
        /// </summary>
        [YamlMember(Alias = "ranges")]
        [JsonProperty("ranges", NullValueHandling = NullValueHandling.Ignore)]
        public List<IDRangeV1Beta1> Ranges { get; set; } = new List<IDRangeV1Beta1>();
    }
}
