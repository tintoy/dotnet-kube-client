using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSetUpdateStrategy indicates the strategy that the StatefulSet controller will use to perform updates. It includes any additional parameters necessary to perform the update for the indicated strategy.
    /// </summary>
    [KubeObject("StatefulSetUpdateStrategy", "v1beta2")]
    public partial class StatefulSetUpdateStrategyV1Beta2
    {
        /// <summary>
        ///     RollingUpdate is used to communicate parameters when Type is RollingUpdateStatefulSetStrategyType.
        /// </summary>
        [JsonProperty("rollingUpdate")]
        public RollingUpdateStatefulSetStrategyV1Beta2 RollingUpdate { get; set; }

        /// <summary>
        ///     Type indicates the type of the StatefulSetUpdateStrategy. Default is RollingUpdate.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
