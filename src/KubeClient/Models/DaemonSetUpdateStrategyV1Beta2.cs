using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetUpdateStrategy is a struct used to control the update strategy for a DaemonSet.
    /// </summary>
    [KubeObject("DaemonSetUpdateStrategy", "v1beta2")]
    public partial class DaemonSetUpdateStrategyV1Beta2
    {
        /// <summary>
        ///     Rolling update config params. Present only if type = "RollingUpdate".
        /// </summary>
        [JsonProperty("rollingUpdate")]
        public RollingUpdateDaemonSetV1Beta2 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is RollingUpdate.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
