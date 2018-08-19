using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.
    /// </summary>
    public partial class LabelSelectorV1
    {
        /// <summary>
        ///     matchExpressions is a list of label selector requirements. The requirements are ANDed.
        /// </summary>
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public List<LabelSelectorRequirementV1> MatchExpressions { get; set; } = new List<LabelSelectorRequirementV1>();

        /// <summary>
        ///     matchLabels is a map of {key,value} pairs. A single {key,value} in the matchLabels map is equivalent to an element of matchExpressions, whose key field is "key", the operator is "In", and the values array contains only "value". The requirements are ANDed.
        /// </summary>
        [JsonProperty("matchLabels", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> MatchLabels { get; set; } = new Dictionary<string, string>();
    }
}
