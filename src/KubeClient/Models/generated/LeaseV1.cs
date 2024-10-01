using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Lease defines a lease concept.
    /// </summary>
    [KubeObject("Lease", "coordination.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/coordination.k8s.io/v1/leases")]
    [KubeApi(KubeAction.WatchList, "apis/coordination.k8s.io/v1/watch/leases")]
    [KubeApi(KubeAction.List, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases")]
    [KubeApi(KubeAction.Create, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases")]
    [KubeApi(KubeAction.Get, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases/{name}")]
    [KubeApi(KubeAction.Patch, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases/{name}")]
    [KubeApi(KubeAction.Delete, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases/{name}")]
    [KubeApi(KubeAction.Update, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/coordination.k8s.io/v1/watch/namespaces/{namespace}/leases")]
    [KubeApi(KubeAction.DeleteCollection, "apis/coordination.k8s.io/v1/namespaces/{namespace}/leases")]
    [KubeApi(KubeAction.Watch, "apis/coordination.k8s.io/v1/watch/namespaces/{namespace}/leases/{name}")]
    public partial class LeaseV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec contains the specification of the Lease. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public LeaseSpecV1 Spec { get; set; }
    }
}
