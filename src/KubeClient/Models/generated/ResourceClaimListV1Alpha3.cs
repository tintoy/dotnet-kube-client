using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimList is a collection of claims.
    /// </summary>
    [KubeListItem("ResourceClaim", "resource.k8s.io/v1alpha3")]
    [KubeObject("ResourceClaimList", "resource.k8s.io/v1alpha3")]
    public partial class ResourceClaimListV1Alpha3 : KubeResourceListV1<ResourceClaimV1Alpha3>
    {
        /// <summary>
        ///     Items is the list of resource claims.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ResourceClaimV1Alpha3> Items { get; } = new List<ResourceClaimV1Alpha3>();
    }
}
