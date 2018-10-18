using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetUpdateStrategy is a struct used to control the update strategy for a DaemonSet.
    /// </summary>
    public partial class DaemonSetUpdateStrategyV1Beta2
    {
        /// <summary>
        ///     Rolling update config params. Present only if type = "RollingUpdate".
        /// </summary>
        [YamlMember(Alias = "rollingUpdate")]
        [JsonProperty("rollingUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public RollingUpdateDaemonSetV1Beta2 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is RollingUpdate.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
