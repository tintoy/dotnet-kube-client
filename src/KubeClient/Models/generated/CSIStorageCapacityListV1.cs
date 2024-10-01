using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSIStorageCapacityList is a collection of CSIStorageCapacity objects.
    /// </summary>
    [KubeListItem("CSIStorageCapacity", "storage.k8s.io/v1")]
    [KubeObject("CSIStorageCapacityList", "storage.k8s.io/v1")]
    public partial class CSIStorageCapacityListV1 : KubeResourceListV1<CSIStorageCapacityV1>
    {
        /// <summary>
        ///     items is the list of CSIStorageCapacity objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CSIStorageCapacityV1> Items { get; } = new List<CSIStorageCapacityV1>();
    }
}
