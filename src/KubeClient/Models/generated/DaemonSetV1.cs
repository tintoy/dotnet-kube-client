using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSet represents the configuration of a daemon set.
    /// </summary>
    [KubeObject("DaemonSet", "apps/v1")]
    [KubeApi(KubeAction.List, "apis/apps/v1/daemonsets")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1/watch/daemonsets")]
    [KubeApi(KubeAction.List, "apis/apps/v1/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Create, "apis/apps/v1/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Update, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1/watch/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apps/v1/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apps/v1/watch/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apps/v1/namespaces/{namespace}/daemonsets/{name}/status")]
    public partial class DaemonSetV1 : KubeResourceV1
    {
        /// <summary>
        ///     The desired behavior of this daemon set. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public DaemonSetSpecV1 Spec { get; set; }

        /// <summary>
        ///     The current status of this daemon set. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public DaemonSetStatusV1 Status { get; set; }
    }
}
