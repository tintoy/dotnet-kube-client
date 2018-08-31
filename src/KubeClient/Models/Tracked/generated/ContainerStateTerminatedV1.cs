using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ContainerStateTerminated is a terminated state of a container.
    /// </summary>
    public partial class ContainerStateTerminatedV1 : Models.ContainerStateTerminatedV1, ITracked
    {
        /// <summary>
        ///     Container's ID in the format 'docker://&lt;container_id&gt;'
        /// </summary>
        [JsonProperty("containerID")]
        [YamlMember(Alias = "containerID")]
        public override string ContainerID
        {
            get
            {
                return base.ContainerID;
            }
            set
            {
                base.ContainerID = value;

                __ModifiedProperties__.Add("ContainerID");
            }
        }


        /// <summary>
        ///     Exit status from the last termination of the container
        /// </summary>
        [JsonProperty("exitCode")]
        [YamlMember(Alias = "exitCode")]
        public override int ExitCode
        {
            get
            {
                return base.ExitCode;
            }
            set
            {
                base.ExitCode = value;

                __ModifiedProperties__.Add("ExitCode");
            }
        }


        /// <summary>
        ///     Message regarding the last termination of the container
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                base.Message = value;

                __ModifiedProperties__.Add("Message");
            }
        }


        /// <summary>
        ///     Signal from the last termination of the container
        /// </summary>
        [JsonProperty("signal")]
        [YamlMember(Alias = "signal")]
        public override int Signal
        {
            get
            {
                return base.Signal;
            }
            set
            {
                base.Signal = value;

                __ModifiedProperties__.Add("Signal");
            }
        }


        /// <summary>
        ///     (brief) reason from the last termination of the container
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public override string Reason
        {
            get
            {
                return base.Reason;
            }
            set
            {
                base.Reason = value;

                __ModifiedProperties__.Add("Reason");
            }
        }


        /// <summary>
        ///     Time at which the container last terminated
        /// </summary>
        [JsonProperty("finishedAt")]
        [YamlMember(Alias = "finishedAt")]
        public override DateTime? FinishedAt
        {
            get
            {
                return base.FinishedAt;
            }
            set
            {
                base.FinishedAt = value;

                __ModifiedProperties__.Add("FinishedAt");
            }
        }


        /// <summary>
        ///     Time at which previous execution of the container started
        /// </summary>
        [JsonProperty("startedAt")]
        [YamlMember(Alias = "startedAt")]
        public override DateTime? StartedAt
        {
            get
            {
                return base.StartedAt;
            }
            set
            {
                base.StartedAt = value;

                __ModifiedProperties__.Add("StartedAt");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
