using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ThirdPartyResourceList is a list of ThirdPartyResources.
    /// </summary>
    [KubeObject("ThirdPartyResourceList", "v1beta1")]
    public class ThirdPartyResourceListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is the list of ThirdPartyResources.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ThirdPartyResourceV1Beta1> Items { get; set; } = new List<ThirdPartyResourceV1Beta1>();
    }
}
