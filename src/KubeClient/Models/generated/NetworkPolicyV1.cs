using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicy describes what network traffic is allowed for a set of Pods
    /// </summary>
    [KubeObject("NetworkPolicy", "v1")]
    [KubeApi("apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies", KubeAction.Create, KubeAction.DeleteCollection)]
    [KubeApi("apis/networking.k8s.io/v1/namespaces/{namespace}/networkpolicies/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("apis/networking.k8s.io/v1/networkpolicies", KubeAction.List)]
    [KubeApi("apis/networking.k8s.io/v1/watch/namespaces/{namespace}/networkpolicies/{name}", KubeAction.Watch)]
    [KubeApi("apis/networking.k8s.io/v1/watch/networkpolicies", KubeAction.WatchList)]
    public partial class NetworkPolicyV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior for this NetworkPolicy.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public NetworkPolicySpecV1 Spec { get; set; }
    }
}
