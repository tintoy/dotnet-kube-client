using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceSliceList is a collection of ResourceSlices.
    /// </summary>
    [KubeListItem("ResourceSlice", "resource.k8s.io/v1alpha3")]
    [KubeObject("ResourceSliceList", "resource.k8s.io/v1alpha3")]
    public partial class ResourceSliceListV1Alpha3 : KubeResourceListV1<ResourceSliceV1Alpha3>
    {
        /// <summary>
        ///     Items is the list of resource ResourceSlices.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ResourceSliceV1Alpha3> Items { get; } = new List<ResourceSliceV1Alpha3>();
    }
}
