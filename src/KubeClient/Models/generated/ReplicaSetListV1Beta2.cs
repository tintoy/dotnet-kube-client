using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicaSetList is a collection of ReplicaSets.
    /// </summary>
    [KubeListItem("ReplicaSet", "apps/v1beta2")]
    [KubeObject("ReplicaSetList", "apps/v1beta2")]
    public partial class ReplicaSetListV1Beta2 : KubeResourceListV1<ReplicaSetV1Beta2>
    {
        /// <summary>
        ///     List of ReplicaSets. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ReplicaSetV1Beta2> Items { get; } = new List<ReplicaSetV1Beta2>();
    }
}
