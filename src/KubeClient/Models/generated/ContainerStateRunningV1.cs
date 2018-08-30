using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStateRunning is a running state of a container.
    /// </summary>
    public partial class ContainerStateRunningV1
    {
        /// <summary>
        ///     Time at which the container was last (re-)started
        /// </summary>
        [JsonProperty("startedAt")]
        [YamlMember(Alias = "startedAt")]
        public virtual DateTime? StartedAt { get; set; }
    }
}
