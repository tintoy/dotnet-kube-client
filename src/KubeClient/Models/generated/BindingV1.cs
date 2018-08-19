using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Binding ties one object to another; for example, a pod is bound to a node by a scheduler. Deprecated in 1.7, please use the bindings subresource of pods instead.
    /// </summary>
    [KubeObject("Binding", "v1")]
    public partial class BindingV1 : KubeResourceV1
    {
        /// <summary>
        ///     The target object that you want to bind to the standard object.
        /// </summary>
        [JsonProperty("target")]
        public ObjectReferenceV1 Target { get; set; }
    }
}
