using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetSpec is a description of a PodDisruptionBudget.
    /// </summary>
    [KubeObject("PodDisruptionBudgetSpec", "v1beta1")]
    public class PodDisruptionBudgetSpecV1Beta1
    {
        /// <summary>
        ///     An eviction is allowed if at most "maxUnavailable" pods selected by "selector" are unavailable after the eviction, i.e. even in absence of the evicted pod. For example, one can prevent all voluntary evictions by specifying 0. This is a mutually exclusive setting with "minAvailable".
        /// </summary>
        [JsonProperty("maxUnavailable")]
        public string MaxUnavailable { get; set; }

        /// <summary>
        ///     An eviction is allowed if at least "minAvailable" pods selected by "selector" will still be available after the eviction, i.e. even in the absence of the evicted pod.  So for example you can prevent all voluntary evictions by specifying "100%".
        /// </summary>
        [JsonProperty("minAvailable")]
        public string MinAvailable { get; set; }

        /// <summary>
        ///     Label query over pods whose evictions are managed by the disruption budget.
        /// </summary>
        [JsonProperty("selector")]
        public LabelSelectorV1 Selector { get; set; }
    }
}
