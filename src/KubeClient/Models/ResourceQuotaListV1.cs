using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuotaList is a list of ResourceQuota items.
    /// </summary>
    [KubeResource("ResourceQuotaList", "v1")]
    public class ResourceQuotaListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of ResourceQuota objects. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_resource_quota.md
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ResourceQuotaV1> Items { get; set; } = new List<ResourceQuotaV1>();
    }
}
