using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerResizePolicy represents resource resize policy for the container.
    /// </summary>
    public partial class ContainerResizePolicyV1
    {
        /// <summary>
        ///     Name of the resource to which this resource resize policy applies. Supported values: cpu, memory.
        /// </summary>
        [YamlMember(Alias = "resourceName")]
        [JsonProperty("resourceName", NullValueHandling = NullValueHandling.Include)]
        public string ResourceName { get; set; }

        /// <summary>
        ///     Restart policy to apply when specified resource is resized. If not specified, it defaults to NotRequired.
        /// </summary>
        [YamlMember(Alias = "restartPolicy")]
        [JsonProperty("restartPolicy", NullValueHandling = NullValueHandling.Include)]
        public string RestartPolicy { get; set; }
    }
}
