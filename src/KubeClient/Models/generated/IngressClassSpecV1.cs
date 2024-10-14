using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressClassSpec provides information about the class of an Ingress.
    /// </summary>
    public partial class IngressClassSpecV1
    {
        /// <summary>
        ///     controller refers to the name of the controller that should handle this class. This allows for different "flavors" that are controlled by the same controller. For example, you may have different parameters for the same implementing controller. This should be specified as a domain-prefixed path no more than 250 characters in length, e.g. "acme.io/ingress-controller". This field is immutable.
        /// </summary>
        [YamlMember(Alias = "controller")]
        [JsonProperty("controller", NullValueHandling = NullValueHandling.Ignore)]
        public string Controller { get; set; }

        /// <summary>
        ///     parameters is a link to a custom resource containing additional configuration for the controller. This is optional if the controller does not require extra parameters.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public IngressClassParametersReferenceV1 Parameters { get; set; }
    }
}
