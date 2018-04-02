using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStateRunning is a running state of a container.
    /// </summary>
    [KubeObject("ContainerStateRunning", "v1")]
    public partial class ContainerStateRunningV1
    {
        /// <summary>
        ///     Time at which the container was last (re-)started
        /// </summary>
        [JsonProperty("startedAt")]
        public DateTime? StartedAt { get; set; }
    }
}
