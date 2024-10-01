using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudgetList is a collection of PodDisruptionBudgets.
    /// </summary>
    [KubeListItem("PodDisruptionBudget", "policy/v1")]
    [KubeObject("PodDisruptionBudgetList", "policy/v1")]
    public partial class PodDisruptionBudgetListV1 : KubeResourceListV1<PodDisruptionBudgetV1>
    {
        /// <summary>
        ///     Items is a list of PodDisruptionBudgets
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodDisruptionBudgetV1> Items { get; } = new List<PodDisruptionBudgetV1>();
    }
}
