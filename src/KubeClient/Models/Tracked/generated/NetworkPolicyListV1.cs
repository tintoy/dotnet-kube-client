using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NetworkPolicyList is a list of NetworkPolicy objects.
    /// </summary>
    [KubeListItem("NetworkPolicy", "networking.k8s.io/v1")]
    [KubeObject("NetworkPolicyList", "networking.k8s.io/v1")]
    public partial class NetworkPolicyListV1 : Models.NetworkPolicyListV1
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.NetworkPolicyV1> Items { get; } = new List<Models.NetworkPolicyV1>();
    }
}
