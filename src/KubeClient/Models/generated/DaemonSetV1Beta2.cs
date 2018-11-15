using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED - This group version of DaemonSet is deprecated by apps/v1/DaemonSet. See the release notes for more information. DaemonSet represents the configuration of a daemon set.
    /// </summary>
    [KubeObject("DaemonSet", "apps/v1beta2")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta2/daemonsets")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta2/watch/daemonsets")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Create, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta2/watch/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apps/v1beta2/watch/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/daemonsets/{name}/status")]
    public partial class DaemonSetV1Beta2 : KubeResourceV1
    {
        /// <summary>
        ///     The desired behavior of this daemon set. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public DaemonSetSpecV1Beta2 Spec { get; set; }

        /// <summary>
        ///     The current status of this daemon set. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public DaemonSetStatusV1Beta2 Status { get; set; }
    }
}
