using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED - This group version of ReplicaSet is deprecated by apps/v1/ReplicaSet. See the release notes for more information. ReplicaSet ensures that a specified number of pod replicas are running at any given time.
    /// </summary>
    [KubeObject("ReplicaSet", "apps/v1beta2")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta2/replicasets")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta2/watch/replicasets")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta2/namespaces/{namespace}/replicasets")]
    [KubeApi(KubeAction.Create, "apis/apps/v1beta2/namespaces/{namespace}/replicasets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta2/watch/namespaces/{namespace}/replicasets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apps/v1beta2/namespaces/{namespace}/replicasets")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apps/v1beta2/watch/namespaces/{namespace}/replicasets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}/status")]
    public partial class ReplicaSetV1Beta2 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the specification of the desired behavior of the ReplicaSet. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ReplicaSetSpecV1Beta2 Spec { get; set; }

        /// <summary>
        ///     Status is the most recently observed status of the ReplicaSet. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ReplicaSetStatusV1Beta2 Status { get; set; }
    }
}
