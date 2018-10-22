using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodPresetSpec is a description of a pod preset.
    /// </summary>
    public partial class PodPresetSpecV1Alpha1
    {
        /// <summary>
        ///     EnvFrom defines the collection of EnvFromSource to inject into containers.
        /// </summary>
        [YamlMember(Alias = "envFrom")]
        [JsonProperty("envFrom", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EnvFromSourceV1> EnvFrom { get; } = new List<EnvFromSourceV1>();

        /// <summary>
        ///     Determine whether the <see cref="EnvFrom"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEnvFrom() => EnvFrom.Count > 0;

        /// <summary>
        ///     Selector is a label query over a set of resources, in this case pods. Required.
        /// </summary>
        [YamlMember(Alias = "selector")]
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 Selector { get; set; }

        /// <summary>
        ///     VolumeMounts defines the collection of VolumeMount to inject into containers.
        /// </summary>
        [YamlMember(Alias = "volumeMounts")]
        [JsonProperty("volumeMounts", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeMountV1> VolumeMounts { get; } = new List<VolumeMountV1>();

        /// <summary>
        ///     Determine whether the <see cref="VolumeMounts"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumeMounts() => VolumeMounts.Count > 0;

        /// <summary>
        ///     Volumes defines the collection of Volume to inject into the pod.
        /// </summary>
        [YamlMember(Alias = "volumes")]
        [JsonProperty("volumes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeV1> Volumes { get; } = new List<VolumeV1>();

        /// <summary>
        ///     Determine whether the <see cref="Volumes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumes() => Volumes.Count > 0;

        /// <summary>
        ///     Env defines the collection of EnvVar to inject into containers.
        /// </summary>
        [YamlMember(Alias = "env")]
        [JsonProperty("env", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<EnvVarV1> Env { get; } = new List<EnvVarV1>();

        /// <summary>
        ///     Determine whether the <see cref="Env"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEnv() => Env.Count > 0;
    }
}
