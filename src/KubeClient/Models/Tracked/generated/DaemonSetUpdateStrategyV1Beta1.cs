using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class DaemonSetUpdateStrategyV1Beta1 : Models.DaemonSetUpdateStrategyV1Beta1, ITracked
    {
        /// <summary>
        ///     Rolling update config params. Present only if type = "RollingUpdate".
        /// </summary>
        [JsonProperty("rollingUpdate")]
        [YamlMember(Alias = "rollingUpdate")]
        public override Models.RollingUpdateDaemonSetV1Beta1 RollingUpdate
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
        ///     Type of daemon set update. Can be "RollingUpdate" or "OnDelete". Default is OnDelete.
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
