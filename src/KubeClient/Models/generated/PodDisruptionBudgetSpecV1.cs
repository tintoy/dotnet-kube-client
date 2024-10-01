using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetSpec is a description of a PodDisruptionBudget.
    /// </summary>
    public partial class PodDisruptionBudgetSpecV1
    {
        /// <summary>
        ///     An eviction is allowed if at most "maxUnavailable" pods selected by "selector" are unavailable after the eviction, i.e. even in absence of the evicted pod. For example, one can prevent all voluntary evictions by specifying 0. This is a mutually exclusive setting with "minAvailable".
        /// </summary>
        [YamlMember(Alias = "maxUnavailable")]
        [JsonProperty("maxUnavailable", NullValueHandling = NullValueHandling.Ignore)]
        public Int32OrStringV1 MaxUnavailable { get; set; }

        /// <summary>
        ///     An eviction is allowed if at least "minAvailable" pods selected by "selector" will still be available after the eviction, i.e. even in the absence of the evicted pod.  So for example you can prevent all voluntary evictions by specifying "100%".
        /// </summary>
        [YamlMember(Alias = "minAvailable")]
        [JsonProperty("minAvailable", NullValueHandling = NullValueHandling.Ignore)]
        public Int32OrStringV1 MinAvailable { get; set; }

        /// <summary>
        ///     Label query over pods whose evictions are managed by the disruption budget. A null selector will match no pods, while an empty ({}) selector will select all pods within the namespace.
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     UnhealthyPodEvictionPolicy defines the criteria for when unhealthy pods should be considered for eviction. Current implementation considers healthy pods, as pods that have status.conditions item with type="Ready",status="True".
        ///     
        ///     Valid policies are IfHealthyBudget and AlwaysAllow. If no policy is specified, the default behavior will be used, which corresponds to the IfHealthyBudget policy.
        ///     
        ///     IfHealthyBudget policy means that running pods (status.phase="Running"), but not yet healthy can be evicted only if the guarded application is not disrupted (status.currentHealthy is at least equal to status.desiredHealthy). Healthy pods will be subject to the PDB for eviction.
        ///     
        ///     AlwaysAllow policy means that all running pods (status.phase="Running"), but not yet healthy are considered disrupted and can be evicted regardless of whether the criteria in a PDB is met. This means perspective running pods of a disrupted application might not get a chance to become healthy. Healthy pods will be subject to the PDB for eviction.
        ///     
        ///     Additional policies may be added in the future. Clients making eviction decisions should disallow eviction of unhealthy pods if they encounter an unrecognized policy in this field.
        ///     
        ///     This field is beta-level. The eviction API uses this field when the feature gate PDBUnhealthyPodEvictionPolicy is enabled (enabled by default).
        /// </summary>
        [YamlMember(Alias = "unhealthyPodEvictionPolicy")]
        [JsonProperty("unhealthyPodEvictionPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string UnhealthyPodEvictionPolicy { get; set; }
    }
}
