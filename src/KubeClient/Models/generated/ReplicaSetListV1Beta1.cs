using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicaSetList is a collection of ReplicaSets.
    /// </summary>
    [KubeObject("ReplicaSetList", "extensions/v1beta1")]
    public class ReplicaSetListV1Beta1 : KubeResourceListV1<ReplicaSetV1Beta1>
    {
        /// <summary>
        ///     List of ReplicaSets. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ReplicaSetV1Beta1> Items { get; } = new List<ReplicaSetV1Beta1>();
    }
}
