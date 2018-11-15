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
        ///     Container's ID in the format 'docker://&lt;container_id&gt;'
        /// </summary>
        [YamlMember(Alias = "containerID")]
        [JsonProperty("containerID", NullValueHandling = NullValueHandling.Ignore)]
        public string ContainerID { get; set; }

        /// <summary>
        ///     Exit status from the last termination of the container
        /// </summary>
        [YamlMember(Alias = "exitCode")]
        [JsonProperty("exitCode", NullValueHandling = NullValueHandling.Include)]
        public int ExitCode { get; set; }

        /// <summary>
        ///     Message regarding the last termination of the container
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     Signal from the last termination of the container
        /// </summary>
        [YamlMember(Alias = "signal")]
        [JsonProperty("signal", NullValueHandling = NullValueHandling.Ignore)]
        public int? Signal { get; set; }

        /// <summary>
        ///     (brief) reason from the last termination of the container
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     Time at which the container last terminated
        /// </summary>
        [YamlMember(Alias = "finishedAt")]
        [JsonProperty("finishedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        ///     Time at which previous execution of the container started
        /// </summary>
        [YamlMember(Alias = "startedAt")]
        [JsonProperty("startedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartedAt { get; set; }
    }
}
