using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Binding ties one object to another; for example, a pod is bound to a node by a scheduler. Deprecated in 1.7, please use the bindings subresource of pods instead.
    /// </summary>
    [KubeObject("Binding", "v1")]
    public partial class BindingV1 : Models.BindingV1
    {
        /// <summary>
        ///     The target object that you want to bind to the standard object.
        /// </summary>
        [JsonProperty("target")]
        [YamlMember(Alias = "target")]
        public override Models.ObjectReferenceV1 Target
        {
            get
            {
                return base.Target;
            }
            set
            {
                base.Target = value;

                __ModifiedProperties__.Add("Target");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
