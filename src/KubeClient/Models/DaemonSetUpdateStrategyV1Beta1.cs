using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeObject("DaemonSetUpdateStrategy", "v1beta1")]
    public partial class DaemonSetUpdateStrategyV1Beta1
    {
        /// <summary>
        ///     Rolling update config params. Present only if type = "RollingUpdate".
        /// </summary>
        [JsonProperty("rollingUpdate")]
        public RollingUpdateDaemonSetV1Beta1 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is OnDelete.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
