using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
    /// </summary>
    public partial class LabelSelectorV1 : Models.LabelSelectorV1, ITracked
    {
        /// <summary>
        ///     matchExpressions is a list of label selector requirements. The requirements are ANDed.
        /// </summary>
        [YamlMember(Alias = "matchExpressions")]
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.LabelSelectorRequirementV1> MatchExpressions { get; set; } = new List<Models.LabelSelectorRequirementV1>();

        /// <summary>
        ///     matchLabels is a map of {key,value} pairs. A single {key,value} in the matchLabels map is equivalent to an element of matchExpressions, whose key field is "key", the operator is "In", and the values array contains only "value". The requirements are ANDed.
        /// </summary>
        [YamlMember(Alias = "matchLabels")]
        [JsonProperty("matchLabels", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> MatchLabels { get; set; } = new Dictionary<string, string>();
    }
}
