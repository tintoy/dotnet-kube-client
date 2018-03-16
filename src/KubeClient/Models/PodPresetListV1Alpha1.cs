using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodPresetList is a list of PodPreset objects.
    /// </summary>
    [KubeObject("PodPresetList", "settings.k8s.io/v1alpha1")]
    public class PodPresetListV1Alpha1 : KubeResourceListV1<PodPresetV1Alpha1>
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodPresetV1Alpha1> Items { get; } = new List<PodPresetV1Alpha1>();
    }
}
