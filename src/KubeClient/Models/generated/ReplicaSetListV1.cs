using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicaSetList is a collection of ReplicaSets.
    /// </summary>
    [KubeListItem("ReplicaSet", "apps/v1")]
    [KubeObject("ReplicaSetList", "apps/v1")]
    public partial class ReplicaSetListV1 : KubeResourceListV1<ReplicaSetV1>
    {
        /// <summary>
        ///     List of ReplicaSets. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ReplicaSetV1> Items { get; } = new List<ReplicaSetV1>();
    }
}
