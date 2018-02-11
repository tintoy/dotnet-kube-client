using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StorageClassList is a collection of storage classes.
    /// </summary>
    [KubeResource("StorageClassList", "v1beta1")]
    public class StorageClassListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is the list of StorageClasses
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<StorageClassV1Beta1> Items { get; set; } = new List<StorageClassV1Beta1>();
    }
}
