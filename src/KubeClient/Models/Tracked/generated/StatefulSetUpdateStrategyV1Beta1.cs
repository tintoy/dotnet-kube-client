using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StatefulSetUpdateStrategy indicates the strategy that the StatefulSet controller will use to perform updates. It includes any additional parameters necessary to perform the update for the indicated strategy.
    /// </summary>
    public partial class StatefulSetUpdateStrategyV1Beta1 : Models.StatefulSetUpdateStrategyV1Beta1, ITracked
    {
        /// <summary>
        ///     RollingUpdate is used to communicate parameters when Type is RollingUpdateStatefulSetStrategyType.
        /// </summary>
        [JsonProperty("rollingUpdate")]
        [YamlMember(Alias = "rollingUpdate")]
        public override Models.RollingUpdateStatefulSetStrategyV1Beta1 RollingUpdate
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
        ///     Type indicates the type of the StatefulSetUpdateStrategy.
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
