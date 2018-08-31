using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ID Range provides a min/max of an allowed range of IDs.
    /// </summary>
    public partial class IDRangeV1Beta1 : Models.IDRangeV1Beta1, ITracked
    {
        /// <summary>
        ///     Min is the start of the range, inclusive.
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
        ///     Max is the end of the range, inclusive.
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
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
