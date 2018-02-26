using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSetUpdateStrategy indicates the strategy that the StatefulSet controller will use to perform updates. It includes any additional parameters necessary to perform the update for the indicated strategy.
    /// </summary>
    [KubeObject("StatefulSetUpdateStrategy", "v1beta1")]
    public class StatefulSetUpdateStrategyV1Beta1
    {
        /// <summary>
        ///     RollingUpdate is used to communicate parameters when Type is RollingUpdateStatefulSetStrategyType.
        /// </summary>
        [JsonProperty("rollingUpdate")]
        public RollingUpdateStatefulSetStrategyV1Beta1 RollingUpdate { get; set; }

        /// <summary>
        ///     Type indicates the type of the StatefulSetUpdateStrategy.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
