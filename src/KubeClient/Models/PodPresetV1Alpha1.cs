using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodPreset is a policy resource that defines additional runtime requirements for a Pod.
    /// </summary>
    [KubeObject("PodPreset", "settings.k8s.io/v1alpha1")]
    public partial class PodPresetV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("spec")]
        public PodPresetSpecV1Alpha1 Spec { get; set; }
    }
}
