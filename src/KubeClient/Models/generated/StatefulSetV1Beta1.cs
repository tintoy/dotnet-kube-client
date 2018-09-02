using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSet represents a set of pods with consistent identities. Identities are defined as:
    ///      - Network: A single stable DNS and hostname.
    ///      - Storage: As many VolumeClaims as requested.
    ///     The StatefulSet guarantees that a given network identity will always map to the same storage identity.
    /// </summary>
    [KubeObject("StatefulSet", "v1beta1")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta1/statefulsets")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta1/watch/statefulsets")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets")]
    [KubeApi(KubeAction.Create, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta1/watch/namespaces/{namespace}/statefulsets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apps/v1beta1/watch/namespaces/{namespace}/statefulsets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}/status")]
    public partial class StatefulSetV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired identities of pods in this set.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public StatefulSetSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is the current status of Pods in this StatefulSet. This data may be out of date by some window of time.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public StatefulSetStatusV1Beta1 Status { get; set; }
    }
}
