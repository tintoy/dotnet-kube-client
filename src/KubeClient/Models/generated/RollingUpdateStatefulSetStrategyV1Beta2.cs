using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RollingUpdateStatefulSetStrategy is used to communicate parameter for RollingUpdateStatefulSetStrategyType.
    /// </summary>
    public partial class RollingUpdateStatefulSetStrategyV1Beta2
    {
        /// <summary>
        ///     Partition indicates the ordinal at which the StatefulSet should be partitioned. Default value is 0.
        /// </summary>
        [YamlMember(Alias = "partition")]
        [JsonProperty("partition", NullValueHandling = NullValueHandling.Ignore)]
        public int? Partition { get; set; }
    }
}
