using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetList is a collection of PodDisruptionBudgets.
    /// </summary>
    [KubeListItem("PodDisruptionBudget", "policy/v1beta1")]
    [KubeObject("PodDisruptionBudgetList", "policy/v1beta1")]
    public partial class PodDisruptionBudgetListV1Beta1 : KubeResourceListV1<PodDisruptionBudgetV1Beta1>
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodDisruptionBudgetV1Beta1> Items { get; } = new List<PodDisruptionBudgetV1Beta1>();
    }
}
