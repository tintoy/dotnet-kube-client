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
    [KubeApi("api/v1/watch/nodes", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/nodes/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/nodes", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("api/v1/nodes/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/nodes/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/proxy/nodes/{name}", KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy)]
    [KubeApi("api/v1/proxy/nodes/{name}/{path}", KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy)]
    [KubeApi("api/v1/nodes/{name}/proxy", KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect)]
    [KubeApi("api/v1/nodes/{name}/proxy/{path}", KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect)]
    public partial class NodeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of a node. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public NodeSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the node. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public NodeStatusV1 Status { get; set; }
    }
}
