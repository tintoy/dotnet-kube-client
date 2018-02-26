using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     LimitRangeList is a list of LimitRange items.
    /// </summary>
    [KubeObject("LimitRangeList", "v1")]
    public class LimitRangeListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of LimitRange objects. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_limit_range.md
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<LimitRangeV1> Items { get; set; } = new List<LimitRangeV1>();
    }
}
