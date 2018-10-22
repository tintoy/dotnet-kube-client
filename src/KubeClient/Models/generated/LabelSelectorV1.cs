using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

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
        [YamlMember(Alias = "matchExpressions")]
        [JsonProperty("matchExpressions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<LabelSelectorRequirementV1> MatchExpressions { get; } = new List<LabelSelectorRequirementV1>();

        /// <summary>
        ///     Determine whether the <see cref="MatchExpressions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMatchExpressions() => MatchExpressions.Count > 0;

        /// <summary>
        ///     matchLabels is a map of {key,value} pairs. A single {key,value} in the matchLabels map is equivalent to an element of matchExpressions, whose key field is "key", the operator is "In", and the values array contains only "value". The requirements are ANDed.
        /// </summary>
        [YamlMember(Alias = "matchLabels")]
        [JsonProperty("matchLabels", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> MatchLabels { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="MatchLabels"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMatchLabels() => MatchLabels.Count > 0;
    }
}
