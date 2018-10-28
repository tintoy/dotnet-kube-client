using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Eviction evicts a pod from its node subject to certain policies and safety constraints. This is a subresource of Pod.  A request to cause such an eviction is created by POSTing to .../pods/&lt;pod name&gt;/evictions.
    /// </summary>
    [KubeObject("Eviction", "policy/v1beta1")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/pods/{name}/eviction")]
    public partial class EvictionV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     DeleteOptions may be provided
        /// </summary>
        [YamlMember(Alias = "deleteOptions")]
        [JsonProperty("deleteOptions", NullValueHandling = NullValueHandling.Ignore)]
        public DeleteOptionsV1 DeleteOptions { get; set; }
    }
}
