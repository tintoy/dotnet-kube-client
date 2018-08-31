using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Host Port Range defines a range of host ports that will be enabled by a policy for pods to use.  It requires both the start and end to be defined.
    /// </summary>
    public partial class HostPortRangeV1Beta1 : Models.HostPortRangeV1Beta1
    {
        /// <summary>
        ///     min is the start of the range, inclusive.
        /// </summary>
        [JsonProperty("min")]
        [YamlMember(Alias = "min")]
        public override int Min
        {
            get
            {
                return base.Min;
            }
            set
            {
                base.Min = value;

                __ModifiedProperties__.Add("Min");
            }
        }


        /// <summary>
        ///     max is the end of the range, inclusive.
        /// </summary>
        [JsonProperty("max")]
        [YamlMember(Alias = "max")]
        public override int Max
        {
            get
            {
                return base.Max;
            }
            set
            {
                base.Max = value;

                __ModifiedProperties__.Add("Max");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
