using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationController represents the configuration of a replication controller.
    /// </summary>
    [KubeObject("ReplicationController", "v1")]
    [KubeApi(KubeAction.List, "api/v1/replicationcontrollers")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/replicationcontrollers")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/replicationcontrollers")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/replicationcontrollers")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/replicationcontrollers")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/replicationcontrollers")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/replicationcontrollers/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}/status")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/replicationcontrollers/{name}/status")]
    public partial class ReplicationControllerV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the specification of the desired behavior of the replication controller. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ReplicationControllerSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status is the most recently observed status of the replication controller. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ReplicationControllerStatusV1 Status { get; set; }
    }
}
