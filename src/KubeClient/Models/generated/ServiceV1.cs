using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Service is a named abstraction of software service (for example, mysql) consisting of local port (for example 3306) that the proxy listens on, and the selector that determines which pods will answer requests sent through the proxy.
    /// </summary>
    [KubeObject("Service", "v1")]
    [KubeApi("api/v1/namespaces/{namespace}/services", KubeAction.Create)]
    [KubeApi("api/v1/namespaces/{namespace}/services/{name}", KubeAction.Delete)]
    [KubeApi("api/v1/namespaces/{namespace}/services/{name}/proxy/{path}", KubeAction.Connect)]
    [KubeApi("api/v1/namespaces/{namespace}/services/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/proxy/namespaces/{namespace}/services/{name}/{path}", KubeAction.Proxy)]
    [KubeApi("api/v1/services", KubeAction.List)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/services/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/watch/services", KubeAction.WatchList)]
    public partial class ServiceV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of a service. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public ServiceSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the service. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public ServiceStatusV1 Status { get; set; }
    }
}
