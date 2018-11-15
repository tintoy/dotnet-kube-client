using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class DaemonSetUpdateStrategyV1Beta1
    {
        /// <summary>
        ///     Rolling update config params. Present only if type = "RollingUpdate".
        /// </summary>
        [YamlMember(Alias = "rollingUpdate")]
        [JsonProperty("rollingUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public RollingUpdateDaemonSetV1Beta1 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is OnDelete.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
