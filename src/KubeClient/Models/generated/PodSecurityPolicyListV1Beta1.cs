using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSecurityPolicyList is a list of PodSecurityPolicy objects.
    /// </summary>
    [KubeListItem("PodSecurityPolicy", "policy/v1beta1")]
    [KubeObject("PodSecurityPolicyList", "policy/v1beta1")]
    public partial class PodSecurityPolicyListV1Beta1 : KubeResourceListV1<PodSecurityPolicyV1Beta1>
    {
        /// <summary>
        ///     items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodSecurityPolicyV1Beta1> Items { get; } = new List<PodSecurityPolicyV1Beta1>();
    }
}
