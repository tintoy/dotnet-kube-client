using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Node is a worker node in Kubernetes. Each node will have a unique identifier in the cache (i.e. in etcd).
    /// </summary>
    [KubeObject("Node", "v1")]
    [KubeApi(KubeAction.List, "api/v1/nodes")]
    [KubeApi(KubeAction.Create, "api/v1/nodes")]
    [KubeApi(KubeAction.Get, "api/v1/nodes/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/nodes/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/nodes/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/nodes/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/nodes")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/nodes")]
    [KubeApi(KubeAction.Get, "api/v1/nodes/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/nodes/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/nodes/{name}/status")]
    [KubeApi(KubeAction.Connect, "api/v1/nodes/{name}/proxy")]
    [KubeApi(KubeAction.Update, "api/v1/nodes/{name}/status")]
    [KubeApi(KubeAction.Connect, "api/v1/nodes/{name}/proxy/{path}")]
    public partial class NodeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of a node. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public NodeSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the node. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public NodeStatusV1 Status { get; set; }
    }
}
