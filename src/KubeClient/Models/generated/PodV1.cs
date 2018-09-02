using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Pod is a collection of containers that can run on a host. This resource is created by clients and scheduled onto hosts.
    /// </summary>
    [KubeObject("Pod", "v1")]
    [KubeApi("api/v1/pods", KubeAction.List)]
    [KubeApi("api/v1/watch/pods", KubeAction.WatchList)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/log", KubeAction.Get)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/pods", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/pods/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/exec", KubeAction.Connect, KubeAction.Connect)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/attach", KubeAction.Connect, KubeAction.Connect)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/portforward", KubeAction.Connect, KubeAction.Connect)]
    [KubeApi("api/v1/namespaces/{namespace}/pods", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/proxy/namespaces/{namespace}/pods/{name}", KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy)]
    [KubeApi("api/v1/proxy/namespaces/{namespace}/pods/{name}/{path}", KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy, KubeAction.Proxy)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/proxy", KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect)]
    [KubeApi("api/v1/namespaces/{namespace}/pods/{name}/proxy/{path}", KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect, KubeAction.Connect)]
    public partial class PodV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the pod. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public PodSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the pod. This data may not be up to date. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public PodStatusV1 Status { get; set; }
    }
}
