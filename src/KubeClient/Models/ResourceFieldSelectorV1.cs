using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceFieldSelector represents container resources (cpu, memory) and their output format
    /// </summary>
    [KubeObject("ResourceFieldSelector", "v1")]
    public class ResourceFieldSelectorV1
    {
        /// <summary>
        ///     Container name: required for volumes, optional for env vars
        /// </summary>
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }

        /// <summary>
        ///     Required: resource to select
        /// </summary>
        [JsonProperty("resource")]
        public string Resource { get; set; }

        /// <summary>
        ///     Specifies the output format of the exposed resources, defaults to "1"
        /// </summary>
        [JsonProperty("divisor")]
        public string Divisor { get; set; }
    }
}
