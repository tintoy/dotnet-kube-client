using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimTemplateList is a collection of claim templates.
    /// </summary>
    [KubeListItem("ResourceClaimTemplate", "resource.k8s.io/v1alpha3")]
    [KubeObject("ResourceClaimTemplateList", "resource.k8s.io/v1alpha3")]
    public partial class ResourceClaimTemplateListV1Alpha3 : KubeResourceListV1<ResourceClaimTemplateV1Alpha3>
    {
        /// <summary>
        ///     Items is the list of resource claim templates.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ResourceClaimTemplateV1Alpha3> Items { get; } = new List<ResourceClaimTemplateV1Alpha3>();
    }
}
