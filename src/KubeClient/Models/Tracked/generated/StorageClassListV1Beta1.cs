using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StorageClassList is a collection of storage classes.
    /// </summary>
    [KubeListItem("StorageClass", "storage.k8s.io/v1beta1")]
    [KubeObject("StorageClassList", "storage.k8s.io/v1beta1")]
    public partial class StorageClassListV1Beta1 : Models.StorageClassListV1Beta1
    {
        /// <summary>
        ///     Items is the list of StorageClasses
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.StorageClassV1Beta1> Items { get; } = new List<Models.StorageClassV1Beta1>();
    }
}
