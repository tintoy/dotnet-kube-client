using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceRequirements describes the compute resource requirements.
    /// </summary>
    [KubeResource("ResourceRequirements", "v1")]
    public class ResourceRequirementsV1
    {
        /// <summary>
        ///     Limits describes the maximum amount of compute resources allowed. More info: https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
        /// </summary>
        [JsonProperty("limits", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Limits { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Requests describes the minimum amount of compute resources required. If Requests is omitted for a container, it defaults to Limits if that is explicitly specified, otherwise to an implementation-defined value. More info: https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
        /// </summary>
        [JsonProperty("requests", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Requests { get; set; } = new Dictionary<string, string>();
    }
}
