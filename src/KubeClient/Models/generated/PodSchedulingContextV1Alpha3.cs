using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSchedulingContext objects hold information that is needed to schedule a Pod with ResourceClaims that use "WaitForFirstConsumer" allocation mode.
    ///     
    ///     This is an alpha type and requires enabling the DRAControlPlaneController feature gate.
    /// </summary>
    [KubeObject("PodSchedulingContext", "resource.k8s.io/v1alpha3")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/podschedulingcontexts")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/podschedulingcontexts")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts")]
    [KubeApi(KubeAction.Create, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}")]
    [KubeApi(KubeAction.Delete, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/namespaces/{namespace}/podschedulingcontexts")]
    [KubeApi(KubeAction.DeleteCollection, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/resource.k8s.io/v1alpha3/watch/namespaces/{namespace}/podschedulingcontexts/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/podschedulingcontexts/{name}/status")]
    public partial class PodSchedulingContextV1Alpha3 : KubeResourceV1
    {
        /// <summary>
        ///     Spec describes where resources for the Pod are needed.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public PodSchedulingContextSpecV1Alpha3 Spec { get; set; }

        /// <summary>
        ///     Status describes where resources for the Pod can be allocated.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public PodSchedulingContextStatusV1Alpha3 Status { get; set; }
    }
}
