using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     RollingUpdateStatefulSetStrategy is used to communicate parameter for RollingUpdateStatefulSetStrategyType.
    /// </summary>
    public partial class RollingUpdateStatefulSetStrategyV1Beta1 : Models.RollingUpdateStatefulSetStrategyV1Beta1
    {
        /// <summary>
        ///     Partition indicates the ordinal at which the StatefulSet should be partitioned.
        /// </summary>
        [JsonProperty("partition")]
        [YamlMember(Alias = "partition")]
        public override int Partition
        {
            get
            {
                return base.Partition;
            }
            set
            {
                base.Partition = value;

                __ModifiedProperties__.Add("Partition");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
