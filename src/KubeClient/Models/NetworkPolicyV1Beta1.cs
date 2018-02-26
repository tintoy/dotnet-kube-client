using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicy describes what network traffic is allowed for a set of Pods
    /// </summary>
    [KubeObject("NetworkPolicy", "extensions/v1beta1")]
    public class NetworkPolicyV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior for this NetworkPolicy.
        /// </summary>
        [JsonProperty("spec")]
        public NetworkPolicySpecV1Beta1 Spec { get; set; }
    }
}
