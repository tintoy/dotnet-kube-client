using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttributesClassList is a collection of VolumeAttributesClass objects.
    /// </summary>
    [KubeListItem("VolumeAttributesClass", "storage.k8s.io/v1alpha1")]
    [KubeObject("VolumeAttributesClassList", "storage.k8s.io/v1alpha1")]
    public partial class VolumeAttributesClassListV1Alpha1 : KubeResourceListV1<VolumeAttributesClassV1Alpha1>
    {
        /// <summary>
        ///     items is the list of VolumeAttributesClass objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<VolumeAttributesClassV1Alpha1> Items { get; } = new List<VolumeAttributesClassV1Alpha1>();
    }
}
