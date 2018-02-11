using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicyList is a list of NetworkPolicy objects.
    /// </summary>
    [KubeResource("NetworkPolicyList", "v1")]
    public class NetworkPolicyListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<NetworkPolicyV1> Items { get; set; } = new List<NetworkPolicyV1>();
    }
}
