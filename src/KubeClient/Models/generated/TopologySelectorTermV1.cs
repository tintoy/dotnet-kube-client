using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A topology selector term represents the result of label queries. A null or empty topology selector term matches no objects. The requirements of them are ANDed. It provides a subset of functionality as NodeSelectorTerm. This is an alpha feature and may change in the future.
    /// </summary>
    public partial class TopologySelectorTermV1
    {
        /// <summary>
        ///     A list of topology selector requirements by labels.
        /// </summary>
        [YamlMember(Alias = "matchLabelExpressions")]
        [JsonProperty("matchLabelExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public List<TopologySelectorLabelRequirementV1> MatchLabelExpressions { get; set; } = new List<TopologySelectorLabelRequirementV1>();
    }
}
