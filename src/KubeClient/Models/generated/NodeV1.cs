using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Node is a worker node in Kubernetes. Each node will have a unique identifier in the cache (i.e. in etcd).
    /// </summary>
    [KubeObject("Node", "v1")]
    public partial class NodeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of a node. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public NodeSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the node. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public NodeStatusV1 Status { get; set; }
    }
}
