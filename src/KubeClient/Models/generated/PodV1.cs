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
    [KubeApi(KubeAction.List, "api/v1/pods")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/pods")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/pods")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/pods")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/pods/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/pods/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/pods/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/pods/{name}")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/pods/{name}/log")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/pods")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/pods")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/pods/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/pods/{name}")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/pods/{name}/exec")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/pods/{name}/status")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/pods/{name}/proxy")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/pods/{name}/status")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/pods/{name}/attach")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/pods/{name}/portforward")]
    [KubeApi(KubeAction.Connect, "api/v1/namespaces/{namespace}/pods/{name}/proxy/{path}")]
    public partial class PodV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the pod. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PodSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the pod. This data may not be up to date. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public PodStatusV1 Status { get; set; }
    }
}
