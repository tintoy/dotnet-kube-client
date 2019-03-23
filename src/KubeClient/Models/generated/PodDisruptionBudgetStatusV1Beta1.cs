using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetStatus represents information about the status of a PodDisruptionBudget. Status may trail the actual state of a system.
    /// </summary>
    public partial class PodDisruptionBudgetStatusV1Beta1
    {
        /// <summary>
        ///     Number of pod disruptions that are currently allowed.
        /// </summary>
        [YamlMember(Alias = "disruptionsAllowed")]
        [JsonProperty("disruptionsAllowed", NullValueHandling = NullValueHandling.Include)]
        public int DisruptionsAllowed { get; set; }

        /// <summary>
        ///     Most recent generation observed when updating this PDB status. PodDisruptionsAllowed and other status informatio is valid only if observedGeneration equals to PDB's object generation.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     DisruptedPods contains information about pods whose eviction was processed by the API server eviction subresource handler but has not yet been observed by the PodDisruptionBudget controller. A pod will be in this map from the time when the API server processed the eviction request to the time when the pod is seen by PDB controller as having been marked for deletion (or after a timeout). The key in the map is the name of the pod and the value is the time when the API server processed the eviction request. If the deletion didn't occur and a pod is still there it will be removed from the list automatically by PodDisruptionBudget controller after some time. If everything goes smooth this map should be empty for the most of the time. Large number of entries in the map may indicate problems with pod deletions.
        /// </summary>
        [YamlMember(Alias = "disruptedPods")]
        [JsonProperty("disruptedPods", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, DateTime> DisruptedPods { get; } = new Dictionary<string, DateTime>();

        /// <summary>
        ///     total number of pods counted by this disruption budget
        /// </summary>
        [YamlMember(Alias = "expectedPods")]
        [JsonProperty("expectedPods", NullValueHandling = NullValueHandling.Include)]
        public int ExpectedPods { get; set; }

        /// <summary>
        ///     current number of healthy pods
        /// </summary>
        [YamlMember(Alias = "currentHealthy")]
        [JsonProperty("currentHealthy", NullValueHandling = NullValueHandling.Include)]
        public int CurrentHealthy { get; set; }

        /// <summary>
        ///     minimum desired number of healthy pods
        /// </summary>
        [YamlMember(Alias = "desiredHealthy")]
        [JsonProperty("desiredHealthy", NullValueHandling = NullValueHandling.Include)]
        public int DesiredHealthy { get; set; }
    }
}
