using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED - This group version of DaemonSet is deprecated by apps/v1beta2/DaemonSet. See the release notes for more information. DaemonSet represents the configuration of a daemon set.
    /// </summary>
    [KubeObject("DaemonSet", "extensions/v1beta1")]
    [KubeApi(KubeAction.List, "apis/extensions/v1beta1/daemonsets")]
    [KubeApi(KubeAction.WatchList, "apis/extensions/v1beta1/watch/daemonsets")]
    [KubeApi(KubeAction.List, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Create, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Get, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Update, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/extensions/v1beta1/watch/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets")]
    [KubeApi(KubeAction.Get, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/extensions/v1beta1/watch/namespaces/{namespace}/daemonsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/extensions/v1beta1/namespaces/{namespace}/daemonsets/{name}/status")]
    public partial class DaemonSetV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     The desired behavior of this daemon set. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public DaemonSetSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     The current status of this daemon set. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public DaemonSetStatusV1Beta1 Status { get; set; }
    }
}
