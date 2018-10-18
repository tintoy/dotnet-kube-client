using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceFieldSelector represents container resources (cpu, memory) and their output format
    /// </summary>
    public partial class ResourceFieldSelectorV1
    {
        /// <summary>
        ///     Container name: required for volumes, optional for env vars
        /// </summary>
        [YamlMember(Alias = "containerName")]
        [JsonProperty("containerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ContainerName { get; set; }

        /// <summary>
        ///     Required: resource to select
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Include)]
        public string Resource { get; set; }

        /// <summary>
        ///     Specifies the output format of the exposed resources, defaults to "1"
        /// </summary>
        [YamlMember(Alias = "divisor")]
        [JsonProperty("divisor", NullValueHandling = NullValueHandling.Ignore)]
        public string Divisor { get; set; }
    }
}
