using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DeploymentStrategy describes how to replace existing pods with new ones.
    /// </summary>
    public partial class DeploymentStrategyV1Beta1 : Models.DeploymentStrategyV1Beta1
    {
        /// <summary>
        ///     Rolling update config params. Present only if DeploymentStrategyType = RollingUpdate.
        /// </summary>
        [JsonProperty("rollingUpdate")]
        [YamlMember(Alias = "rollingUpdate")]
        public override Models.RollingUpdateDeploymentV1Beta1 RollingUpdate
        {
            get
            {
                return base.RollingUpdate;
            }
            set
            {
                base.RollingUpdate = value;

                __ModifiedProperties__.Add("RollingUpdate");
            }
        }


        /// <summary>
        ///     Type of deployment. Can be "Recreate" or "RollingUpdate". Default is RollingUpdate.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
