using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSchedulingContextSpec describes where resources for the Pod are needed.
    /// </summary>
    public partial class PodSchedulingContextSpecV1Alpha3
    {
        /// <summary>
        ///     SelectedNode is the node for which allocation of ResourceClaims that are referenced by the Pod and that use "WaitForFirstConsumer" allocation is to be attempted.
        /// </summary>
        [YamlMember(Alias = "selectedNode")]
        [JsonProperty("selectedNode", NullValueHandling = NullValueHandling.Ignore)]
        public string SelectedNode { get; set; }

        /// <summary>
        ///     PotentialNodes lists nodes where the Pod might be able to run.
        ///     
        ///     The size of this field is limited to 128. This is large enough for many clusters. Larger clusters may need more attempts to find a node that suits all pending resources. This may get increased in the future, but not reduced.
        /// </summary>
        [YamlMember(Alias = "potentialNodes")]
        [JsonProperty("potentialNodes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> PotentialNodes { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="PotentialNodes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePotentialNodes() => PotentialNodes.Count > 0;
    }
}
