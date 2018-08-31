using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodPreset is a policy resource that defines additional runtime requirements for a Pod.
    /// </summary>
    [KubeObject("PodPreset", "settings.k8s.io/v1alpha1")]
    public partial class PodPresetV1Alpha1 : Models.PodPresetV1Alpha1, ITracked
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.PodPresetSpecV1Alpha1 Spec
        {
            get
            {
                return base.Spec;
            }
            set
            {
                base.Spec = value;

                __ModifiedProperties__.Add("Spec");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
