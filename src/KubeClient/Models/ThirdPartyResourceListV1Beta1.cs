using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ThirdPartyResourceList is a list of ThirdPartyResources.
    /// </summary>
    [KubeObject("ThirdPartyResourceList", "extensions/v1beta1")]
    public class ThirdPartyResourceListV1Beta1 : KubeResourceListV1<ThirdPartyResourceV1Beta1>
    {
        /// <summary>
        ///     Items is the list of ThirdPartyResources.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ThirdPartyResourceV1Beta1> Items { get; } = new List<ThirdPartyResourceV1Beta1>();
    }
}
