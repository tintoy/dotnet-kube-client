using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Namespace provides a scope for Names. Use of multiple namespaces is optional.
    /// </summary>
    [KubeObject("Namespace", "v1")]
    [KubeApi(KubeAction.List, "api/v1/namespaces")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{name}/status")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{name}/status")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{name}/finalize")]
    public partial class NamespaceV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of the Namespace. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public NamespaceSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status describes the current status of a Namespace. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public NamespaceStatusV1 Status { get; set; }
    }
}
