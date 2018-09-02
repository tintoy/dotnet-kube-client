using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationControllerList is a collection of replication controllers.
    /// </summary>
    [KubeListItem("ReplicationController", "v1")]
    [KubeObject("ReplicationControllerList", "v1")]
    public partial class ReplicationControllerListV1 : KubeResourceListV1<ReplicationControllerV1>
    {
        /// <summary>
        ///     List of replication controllers. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ReplicationControllerV1> Items { get; } = new List<ReplicationControllerV1>();
    }
}
