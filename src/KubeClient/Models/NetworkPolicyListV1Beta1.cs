using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Network Policy List is a list of NetworkPolicy objects.
    /// </summary>
    [KubeObject("NetworkPolicyList", "v1beta1")]
    public class NetworkPolicyListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<NetworkPolicyV1Beta1> Items { get; set; } = new List<NetworkPolicyV1Beta1>();
    }
}
