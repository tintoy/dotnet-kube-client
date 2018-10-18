using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Binding ties one object to another; for example, a pod is bound to a node by a scheduler. Deprecated in 1.7, please use the bindings subresource of pods instead.
    /// </summary>
    [KubeObject("Binding", "v1")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/bindings")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/pods/{name}/binding")]
    public partial class BindingV1 : KubeResourceV1
    {
        /// <summary>
        ///     The target object that you want to bind to the standard object.
        /// </summary>
        [YamlMember(Alias = "target")]
        [JsonProperty("target", NullValueHandling = NullValueHandling.Include)]
        public ObjectReferenceV1 Target { get; set; }
    }
}
