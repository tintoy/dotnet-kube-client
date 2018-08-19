using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStateTerminated is a terminated state of a container.
    /// </summary>
    public partial class ContainerStateTerminatedV1
    {
        /// <summary>
        ///     Container's ID in the format 'docker://&lt;container_id&gt;'
        /// </summary>
        [JsonProperty("containerID")]
        public string ContainerID { get; set; }

        /// <summary>
        ///     Exit status from the last termination of the container
        /// </summary>
        [JsonProperty("exitCode")]
        public int ExitCode { get; set; }

        /// <summary>
        ///     Message regarding the last termination of the container
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Signal from the last termination of the container
        /// </summary>
        [JsonProperty("signal")]
        public int Signal { get; set; }

        /// <summary>
        ///     (brief) reason from the last termination of the container
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Time at which the container last terminated
        /// </summary>
        [JsonProperty("finishedAt")]
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        ///     Time at which previous execution of the container started
        /// </summary>
        [JsonProperty("startedAt")]
        public DateTime? StartedAt { get; set; }
    }
}
