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
    [KubeApi(KubeAction.List, "api/v1/services")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/services")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/services")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/services")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/services/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/services/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/services/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/services/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/services")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/services/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/services/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/services/{name}/status")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/services/{name}/proxy")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/services/{name}/status")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/services/{name}/proxy/{path}")]
    public partial class ServiceV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of a service. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the service. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceStatusV1 Status { get; set; }
    }
}
