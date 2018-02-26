using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetList is a collection of PodDisruptionBudgets.
    /// </summary>
    [KubeObject("PodDisruptionBudgetList", "v1beta1")]
    public class PodDisruptionBudgetListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<PodDisruptionBudgetV1Beta1> Items { get; set; } = new List<PodDisruptionBudgetV1Beta1>();
    }
}
