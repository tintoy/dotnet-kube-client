using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuotaList is a list of ResourceQuota items.
    /// </summary>
    [KubeListItem("ResourceQuota", "v1")]
    [KubeObject("ResourceQuotaList", "v1")]
    public partial class ResourceQuotaListV1 : KubeResourceListV1<ResourceQuotaV1>
    {
        /// <summary>
        ///     Items is a list of ResourceQuota objects. More info: https://kubernetes.io/docs/concepts/policy/resource-quotas/
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ResourceQuotaV1> Items { get; } = new List<ResourceQuotaV1>();
    }
}
