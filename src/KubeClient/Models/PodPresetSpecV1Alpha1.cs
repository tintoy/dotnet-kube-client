using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodPresetSpec is a description of a pod preset.
    /// </summary>
    [KubeObject("PodPresetSpec", "v1alpha1")]
    public partial class PodPresetSpecV1Alpha1
    {
        /// <summary>
        ///     EnvFrom defines the collection of EnvFromSource to inject into containers.
        /// </summary>
        [JsonProperty("envFrom", NullValueHandling = NullValueHandling.Ignore)]
        public List<EnvFromSourceV1> EnvFrom { get; set; } = new List<EnvFromSourceV1>();

        /// <summary>
        ///     Selector is a label query over a set of resources, in this case pods. Required.
        /// </summary>
        [JsonProperty("selector")]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     VolumeMounts defines the collection of VolumeMount to inject into containers.
        /// </summary>
        [JsonProperty("volumeMounts", NullValueHandling = NullValueHandling.Ignore)]
        public List<VolumeMountV1> VolumeMounts { get; set; } = new List<VolumeMountV1>();

        /// <summary>
        ///     Volumes defines the collection of Volume to inject into the pod.
        /// </summary>
        [JsonProperty("volumes", NullValueHandling = NullValueHandling.Ignore)]
        public List<VolumeV1> Volumes { get; set; } = new List<VolumeV1>();

        /// <summary>
        ///     Env defines the collection of EnvVar to inject into containers.
        /// </summary>
        [JsonProperty("env", NullValueHandling = NullValueHandling.Ignore)]
        public List<EnvVarV1> Env { get; set; } = new List<EnvVarV1>();
    }
}
