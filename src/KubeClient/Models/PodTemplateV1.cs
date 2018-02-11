using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodTemplate describes a template for creating copies of a predefined pod.
    /// </summary>
    [KubeResource("PodTemplate", "v1")]
    public class PodTemplateV1 : KubeResourceV1
    {
        /// <summary>
        ///     Template defines the pods that will be created from this pod template. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("template")]
        public PodTemplateSpecV1 Template { get; set; }
    }
}
