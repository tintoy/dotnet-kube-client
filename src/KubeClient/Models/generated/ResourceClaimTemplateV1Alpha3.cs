using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimTemplate is used to produce ResourceClaim objects.
    ///     
    ///     This is an alpha type and requires enabling the DynamicResourceAllocation feature gate.
    /// </summary>
    [KubeObject("ResourceClaimTemplate", "resource.k8s.io/v1alpha3")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/resourceclaimtemplates")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/resourceclaimtemplates")]
    [KubeApi(KubeAction.List, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates")]
    [KubeApi(KubeAction.Create, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates")]
    [KubeApi(KubeAction.Get, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates/{name}")]
    [KubeApi(KubeAction.Patch, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates/{name}")]
    [KubeApi(KubeAction.Delete, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates/{name}")]
    [KubeApi(KubeAction.Update, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/resource.k8s.io/v1alpha3/watch/namespaces/{namespace}/resourceclaimtemplates")]
    [KubeApi(KubeAction.DeleteCollection, "apis/resource.k8s.io/v1alpha3/namespaces/{namespace}/resourceclaimtemplates")]
    [KubeApi(KubeAction.Watch, "apis/resource.k8s.io/v1alpha3/watch/namespaces/{namespace}/resourceclaimtemplates/{name}")]
    public partial class ResourceClaimTemplateV1Alpha3 : KubeResourceV1
    {
        /// <summary>
        ///     Describes the ResourceClaim that is to be generated.
        ///     
        ///     This field is immutable. A ResourceClaim will get created by the control plane for a Pod when needed and then not get updated anymore.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public ResourceClaimTemplateSpecV1Alpha3 Spec { get; set; }
    }
}
