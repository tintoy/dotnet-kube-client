using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetUpdateStrategy is a struct used to control the update strategy for a DaemonSet.
    /// </summary>
    public partial class DaemonSetUpdateStrategyV1
    {
        /// <summary>
        ///     Rolling update config params. Present only if type = "RollingUpdate".
        /// </summary>
        [JsonProperty("rollingUpdate")]
        [YamlMember(Alias = "rollingUpdate")]
        public RollingUpdateDaemonSetV1 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is RollingUpdate.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }
    }
}
