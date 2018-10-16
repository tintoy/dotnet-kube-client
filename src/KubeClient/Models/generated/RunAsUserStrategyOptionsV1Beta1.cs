using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RunAsUserStrategyOptions defines the strategy type and any options used to create the strategy. Deprecated: use RunAsUserStrategyOptions from policy API Group instead.
    /// </summary>
    public partial class RunAsUserStrategyOptionsV1Beta1
    {
        /// <summary>
        ///     rule is the strategy that will dictate the allowable RunAsUser values that may be set.
        /// </summary>
        [JsonProperty("rule")]
        [YamlMember(Alias = "rule")]
        public string Rule { get; set; }

        /// <summary>
        ///     ranges are the allowed ranges of uids that may be used. If you would like to force a single uid then supply a single range with the same start and end. Required for MustRunAs.
        /// </summary>
        [YamlMember(Alias = "ranges")]
        [JsonProperty("ranges", NullValueHandling = NullValueHandling.Ignore)]
        public List<IDRangeV1Beta1> Ranges { get; set; } = new List<IDRangeV1Beta1>();
    }
}
