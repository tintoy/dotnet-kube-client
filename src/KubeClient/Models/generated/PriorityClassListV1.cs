using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityClassList is a collection of priority classes.
    /// </summary>
    [KubeListItem("PriorityClass", "scheduling.k8s.io/v1")]
    [KubeObject("PriorityClassList", "scheduling.k8s.io/v1")]
    public partial class PriorityClassListV1 : KubeResourceListV1<PriorityClassV1>
    {
        /// <summary>
        ///     items is the list of PriorityClasses
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PriorityClassV1> Items { get; } = new List<PriorityClassV1>();
    }
}
