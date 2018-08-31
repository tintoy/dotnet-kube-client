using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StorageClassList is a collection of storage classes.
    /// </summary>
    [KubeListItem("StorageClass", "storage.k8s.io/v1")]
    [KubeObject("StorageClassList", "storage.k8s.io/v1")]
    public partial class StorageClassListV1 : Models.StorageClassListV1, ITracked
    {
        /// <summary>
        ///     Items is the list of StorageClasses
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.StorageClassV1> Items { get; } = new List<Models.StorageClassV1>();
    }
}
