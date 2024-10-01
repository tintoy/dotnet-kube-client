using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LeaseCandidate defines a candidate for a Lease object. Candidates are created such that coordinated leader election will pick the best leader from the list of candidates.
    /// </summary>
    [KubeObject("LeaseCandidate", "coordination.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/coordination.k8s.io/v1alpha1/leasecandidates")]
    [KubeApi(KubeAction.WatchList, "apis/coordination.k8s.io/v1alpha1/watch/leasecandidates")]
    [KubeApi(KubeAction.List, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates")]
    [KubeApi(KubeAction.Create, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates")]
    [KubeApi(KubeAction.Get, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates/{name}")]
    [KubeApi(KubeAction.Patch, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates/{name}")]
    [KubeApi(KubeAction.Delete, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates/{name}")]
    [KubeApi(KubeAction.Update, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/coordination.k8s.io/v1alpha1/watch/namespaces/{namespace}/leasecandidates")]
    [KubeApi(KubeAction.DeleteCollection, "apis/coordination.k8s.io/v1alpha1/namespaces/{namespace}/leasecandidates")]
    [KubeApi(KubeAction.Watch, "apis/coordination.k8s.io/v1alpha1/watch/namespaces/{namespace}/leasecandidates/{name}")]
    public partial class LeaseCandidateV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     spec contains the specification of the Lease. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public LeaseCandidateSpecV1Alpha1 Spec { get; set; }
    }
}
