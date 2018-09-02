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
    [KubeApi("api/v1/watch/namespaces", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/namespaces/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/namespaces/{name}/finalize", KubeAction.Update)]
    [KubeApi("api/v1/namespaces", KubeAction.Create, KubeAction.List)]
    [KubeApi("api/v1/namespaces/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/namespaces/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    public partial class NamespaceV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of the Namespace. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public NamespaceSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status describes the current status of a Namespace. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public NamespaceStatusV1 Status { get; set; }
    }
}
