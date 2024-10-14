using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaim describes a request for access to resources in the cluster, for use by workloads. For example, if a workload needs an accelerator device with specific properties, this is how that request is expressed. The status stanza tracks whether this claim has been satisfied and what specific resources have been allocated.
    ///     
    ///     This is an alpha type and requires enabling the DynamicResourceAllocation feature gate.
    /// </summary>
    [KubeObject("ResourceClaim", "resource.k8s.io/v1alpha3")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/resourceclaims")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/resourceclaims")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims")]
    [KubeApi(KubeAction.Create, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}")]
    [KubeApi(KubeAction.Delete, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/namespaces/{namespace}/resourceclaims")]
    [KubeApi(KubeAction.DeleteCollection, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/resource.k8s.io/v1alpha3/watch/namespaces/{namespace}/resourceclaims/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaims/{name}/status")]
    public partial class ResourceClaimV1Alpha3 : KubeResourceV1
    {
        /// <summary>
        ///     Spec describes what is being requested and how to configure it. The spec is immutable.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public ResourceClaimSpecV1Alpha3 Spec { get; set; }

        /// <summary>
        ///     Status describes whether the claim is ready to use and what has been allocated.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceClaimStatusV1Alpha3 Status { get; set; }
    }
}
