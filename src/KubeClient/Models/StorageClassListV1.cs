using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StorageClassList is a collection of storage classes.
    /// </summary>
    [KubeObject("StorageClassList", "storage.k8s.io/v1")]
    public class StorageClassListV1 : KubeResourceListV1<StorageClassV1>
    {
        /// <summary>
        ///     Items is the list of StorageClasses
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<StorageClassV1> Items { get; } = new List<StorageClassV1>();
    }
}
