using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressClass represents the class of the Ingress, referenced by the Ingress Spec. The `ingressclass.kubernetes.io/is-default-class` annotation can be used to indicate that an IngressClass should be considered default. When a single IngressClass resource has this annotation set to true, new Ingress resources without a class specified will be assigned this default class.
    /// </summary>
    [KubeObject("IngressClass", "networking.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1/ingressclasses")]
    [KubeApi(KubeAction.Create, "apis/networking.k8s.io/v1/ingressclasses")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1/ingressclasses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1/ingressclasses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/networking.k8s.io/v1/ingressclasses/{name}")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1/ingressclasses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1/watch/ingressclasses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/networking.k8s.io/v1/ingressclasses")]
    [KubeApi(KubeAction.Watch, "apis/networking.k8s.io/v1/watch/ingressclasses/{name}")]
    public partial class IngressClassV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the desired state of the IngressClass. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public IngressClassSpecV1 Spec { get; set; }
    }
}
