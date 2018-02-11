using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationControllerList is a collection of replication controllers.
    /// </summary>
    [KubeResource("ReplicationControllerList", "v1")]
    public class ReplicationControllerListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of replication controllers. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReplicationControllerV1> Items { get; set; } = new List<ReplicationControllerV1>();
    }
}
