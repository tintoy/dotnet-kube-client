using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodPresetSpec is a description of a pod preset.
    /// </summary>
    public partial class PodPresetSpecV1Alpha1 : Models.PodPresetSpecV1Alpha1, ITracked
    {
        /// <summary>
        ///     EnvFrom defines the collection of EnvFromSource to inject into containers.
        /// </summary>
        [YamlMember(Alias = "envFrom")]
        [JsonProperty("envFrom", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EnvFromSourceV1> EnvFrom { get; set; } = new List<Models.EnvFromSourceV1>();

        /// <summary>
        ///     Selector is a label query over a set of resources, in this case pods. Required.
        /// </summary>
        [JsonProperty("selector")]
        [YamlMember(Alias = "selector")]
        public override Models.LabelSelectorV1 Selector
        {
            get
            {
                return base.Selector;
            }
            set
            {
                base.Selector = value;

                __ModifiedProperties__.Add("Selector");
            }
        }


        /// <summary>
        ///     VolumeMounts defines the collection of VolumeMount to inject into containers.
        /// </summary>
        [YamlMember(Alias = "volumeMounts")]
        [JsonProperty("volumeMounts", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.VolumeMountV1> VolumeMounts { get; set; } = new List<Models.VolumeMountV1>();

        /// <summary>
        ///     Volumes defines the collection of Volume to inject into the pod.
        /// </summary>
        [YamlMember(Alias = "volumes")]
        [JsonProperty("volumes", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.VolumeV1> Volumes { get; set; } = new List<Models.VolumeV1>();

        /// <summary>
        ///     Env defines the collection of EnvVar to inject into containers.
        /// </summary>
        [YamlMember(Alias = "env")]
        [JsonProperty("env", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EnvVarV1> Env { get; set; } = new List<Models.EnvVarV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
