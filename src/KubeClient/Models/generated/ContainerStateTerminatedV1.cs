using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStateTerminated is a terminated state of a container.
    /// </summary>
    public partial class ContainerStateTerminatedV1
    {
        /// <summary>
        ///     Exit status from the last termination of the container
        /// </summary>
        [JsonProperty("exitCode")]
        [YamlMember(Alias = "exitCode")]
        public int ExitCode { get; set; }

        /// <summary>
        ///     Time at which the container last terminated
        /// </summary>
        [JsonProperty("finishedAt")]
        [YamlMember(Alias = "finishedAt")]
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        ///     Signal from the last termination of the container
        /// </summary>
        [JsonProperty("signal")]
        [YamlMember(Alias = "signal")]
        public int Signal { get; set; }

        /// <summary>
        ///     Time at which previous execution of the container started
        /// </summary>
        [JsonProperty("startedAt")]
        [YamlMember(Alias = "startedAt")]
        public DateTime? StartedAt { get; set; }

        /// <summary>
        ///     Container's ID in the format 'docker://&lt;container_id&gt;'
        /// </summary>
        [JsonProperty("containerID")]
        [YamlMember(Alias = "containerID")]
        public string ContainerID { get; set; }

        /// <summary>
        ///     (brief) reason from the last termination of the container
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Message regarding the last termination of the container
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public string Message { get; set; }
    }
}
