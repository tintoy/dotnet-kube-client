using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentStrategy describes how to replace existing pods with new ones.
    /// </summary>
    public partial class DeploymentStrategyV1Beta2
    {
        /// <summary>
        ///     Rolling update config params. Present only if DeploymentStrategyType = RollingUpdate.
        /// </summary>
        [YamlMember(Alias = "rollingUpdate")]
        [JsonProperty("rollingUpdate", NullValueHandling = NullValueHandling.Ignore)]
        public RollingUpdateDeploymentV1Beta2 RollingUpdate { get; set; }

        /// <summary>
        ///     Type of deployment. Can be "Recreate" or "RollingUpdate". Default is RollingUpdate.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
