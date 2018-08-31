using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodPresetList is a list of PodPreset objects.
    /// </summary>
    [KubeListItem("PodPreset", "settings.k8s.io/v1alpha1")]
    [KubeObject("PodPresetList", "settings.k8s.io/v1alpha1")]
    public partial class PodPresetListV1Alpha1 : Models.PodPresetListV1Alpha1
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.PodPresetV1Alpha1> Items { get; } = new List<Models.PodPresetV1Alpha1>();
    }
}
