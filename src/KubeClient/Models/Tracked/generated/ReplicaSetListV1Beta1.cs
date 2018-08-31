using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ReplicaSetList is a collection of ReplicaSets.
    /// </summary>
    [KubeListItem("ReplicaSet", "extensions/v1beta1")]
    [KubeObject("ReplicaSetList", "extensions/v1beta1")]
    public partial class ReplicaSetListV1Beta1 : Models.ReplicaSetListV1Beta1, ITracked
    {
        /// <summary>
        ///     List of ReplicaSets. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ReplicaSetV1Beta1> Items { get; } = new List<Models.ReplicaSetV1Beta1>();
    }
}
