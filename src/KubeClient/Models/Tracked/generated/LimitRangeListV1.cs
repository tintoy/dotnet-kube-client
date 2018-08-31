using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     LimitRangeList is a list of LimitRange items.
    /// </summary>
    [KubeListItem("LimitRange", "v1")]
    [KubeObject("LimitRangeList", "v1")]
    public partial class LimitRangeListV1 : Models.LimitRangeListV1, ITracked
    {
        /// <summary>
        ///     Items is a list of LimitRange objects. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_limit_range.md
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.LimitRangeV1> Items { get; } = new List<Models.LimitRangeV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
