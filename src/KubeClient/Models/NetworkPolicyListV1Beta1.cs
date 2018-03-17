using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Network Policy List is a list of NetworkPolicy objects.
    /// </summary>
    [KubeObject("NetworkPolicyList", "extensions/v1beta1")]
    public class NetworkPolicyListV1Beta1 : KubeResourceListV1<NetworkPolicyV1Beta1>
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<NetworkPolicyV1Beta1> Items { get; } = new List<NetworkPolicyV1Beta1>();
    }
}
