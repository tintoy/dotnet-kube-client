using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RollingUpdateStatefulSetStrategy is used to communicate parameter for RollingUpdateStatefulSetStrategyType.
    /// </summary>
    [KubeResource("RollingUpdateStatefulSetStrategy", "v1beta1")]
    public class RollingUpdateStatefulSetStrategyV1Beta1
    {
        /// <summary>
        ///     Partition indicates the ordinal at which the StatefulSet should be partitioned.
        /// </summary>
        [JsonProperty("partition")]
        public int Partition { get; set; }
    }
}
