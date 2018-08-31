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
    public partial class LimitRangeListV1 : Models.LimitRangeListV1
    {
        /// <summary>
        ///     Items is a list of LimitRange objects. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_limit_range.md
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.LimitRangeV1> Items { get; } = new List<Models.LimitRangeV1>();
    }
}
