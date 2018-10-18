using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSetUpdateStrategy indicates the strategy that the StatefulSet controller will use to perform updates. It includes any additional parameters necessary to perform the update for the indicated strategy.
    /// </summary>
    public partial class StatefulSetUpdateStrategyV1Beta1
    {
        /// <summary>
        ///     RollingUpdate is used to communicate parameters when Type is RollingUpdateStatefulSetStrategyType.
        /// </summary>
        [YamlMember(Alias = "rollingUpdate")]
        [JsonProperty("rollingUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public RollingUpdateStatefulSetStrategyV1Beta1 RollingUpdate { get; set; }

        /// <summary>
        ///     Type indicates the type of the StatefulSetUpdateStrategy.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
