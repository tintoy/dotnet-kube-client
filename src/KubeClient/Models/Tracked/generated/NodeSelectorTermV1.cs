using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A null or empty node selector term matches no objects.
    /// </summary>
    public partial class NodeSelectorTermV1 : Models.NodeSelectorTermV1, ITracked
    {
        /// <summary>
        ///     Required. A list of node selector requirements. The requirements are ANDed.
        /// </summary>
        [YamlMember(Alias = "matchExpressions")]
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.NodeSelectorRequirementV1> MatchExpressions { get; set; } = new List<Models.NodeSelectorRequirementV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
