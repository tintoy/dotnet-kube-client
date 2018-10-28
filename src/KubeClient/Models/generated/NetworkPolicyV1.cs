using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicy describes what network traffic is allowed for a set of Pods
    /// </summary>
    [KubeObject("NetworkPolicy", "networking.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1/networkpolicies")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1/watch/networkpolicies")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies")]
    [KubeApi(KubeAction.Create, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies/{name}")]
    [KubeApi(KubeAction.Delete, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies/{name}")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1/watch/namespaces/{namespace}/networkpolicies")]
    [KubeApi(KubeAction.DeleteCollection, "apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies")]
    [KubeApi(KubeAction.Watch, "apis/networking.k8s.io/v1/watch/namespaces/{namespace}/networkpolicies/{name}")]
    public partial class NetworkPolicyV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior for this NetworkPolicy.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public NetworkPolicySpecV1 Spec { get; set; }
    }
}
