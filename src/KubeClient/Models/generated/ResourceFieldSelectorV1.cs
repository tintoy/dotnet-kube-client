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
        ///     Required: resource to select
        /// </summary>
        [JsonProperty("resource")]
        [YamlMember(Alias = "resource")]
        public string Resource { get; set; }

        /// <summary>
        ///     Specifies the output format of the exposed resources, defaults to "1"
        /// </summary>
        [JsonProperty("divisor")]
        [YamlMember(Alias = "divisor")]
        public string Divisor { get; set; }

        /// <summary>
        ///     Container name: required for volumes, optional for env vars
        /// </summary>
        [JsonProperty("containerName")]
        [YamlMember(Alias = "containerName")]
        public string ContainerName { get; set; }
    }
}
