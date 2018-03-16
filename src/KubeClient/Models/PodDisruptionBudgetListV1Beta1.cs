using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetList is a collection of PodDisruptionBudgets.
    /// </summary>
    [KubeObject("PodDisruptionBudgetList", "policy/v1beta1")]
    public class PodDisruptionBudgetListV1Beta1 : KubeResourceListV1<PodDisruptionBudgetV1Beta1>
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodDisruptionBudgetV1Beta1> Items { get; } = new List<PodDisruptionBudgetV1Beta1>();
    }
}
