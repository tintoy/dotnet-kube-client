using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSecurityPolicy governs the ability to make requests that affect the Security Context that will be applied to a pod and container.
    /// </summary>
    [KubeObject("PodSecurityPolicy", "policy/v1beta1")]
    [KubeApi(KubeAction.List, "apis/policy/v1beta1/podsecuritypolicies")]
    [KubeApi(KubeAction.Create, "apis/policy/v1beta1/podsecuritypolicies")]
    [KubeApi(KubeAction.Get, "apis/policy/v1beta1/podsecuritypolicies/{name}")]
    [KubeApi(KubeAction.Patch, "apis/policy/v1beta1/podsecuritypolicies/{name}")]
    [KubeApi(KubeAction.Delete, "apis/policy/v1beta1/podsecuritypolicies/{name}")]
    [KubeApi(KubeAction.Update, "apis/policy/v1beta1/podsecuritypolicies/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/policy/v1beta1/watch/podsecuritypolicies")]
    [KubeApi(KubeAction.DeleteCollection, "apis/policy/v1beta1/podsecuritypolicies")]
    [KubeApi(KubeAction.Watch, "apis/policy/v1beta1/watch/podsecuritypolicies/{name}")]
    public partial class PodSecurityPolicyV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     spec defines the policy enforced.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PodSecurityPolicySpecV1Beta1 Spec { get; set; }
    }
}
