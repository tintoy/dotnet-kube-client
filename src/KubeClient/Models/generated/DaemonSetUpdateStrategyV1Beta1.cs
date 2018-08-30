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
        [JsonProperty("rollingUpdate")]
        [YamlMember(Alias = "rollingUpdate")]
        public virtual RollingUpdateDaemonSetV1Beta1 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is OnDelete.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }
    }
}
