using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ContainerState holds a possible state of container. Only one of its members may be specified. If none of them is specified, the default one is ContainerStateWaiting.
    /// </summary>
    public partial class ContainerStateV1 : Models.ContainerStateV1
    {
        /// <summary>
        ///     Details about a terminated container
        /// </summary>
        [JsonProperty("terminated")]
        [YamlMember(Alias = "terminated")]
        public override Models.ContainerStateTerminatedV1 Terminated
        {
            get
            {
                return base.Terminated;
            }
            set
            {
                base.Terminated = value;

                __ModifiedProperties__.Add("Terminated");
            }
        }


        /// <summary>
        ///     Details about a running container
        /// </summary>
        [JsonProperty("running")]
        [YamlMember(Alias = "running")]
        public override Models.ContainerStateRunningV1 Running
        {
            get
            {
                return base.Running;
            }
            set
            {
                base.Running = value;

                __ModifiedProperties__.Add("Running");
            }
        }


        /// <summary>
        ///     Details about a waiting container
        /// </summary>
        [JsonProperty("waiting")]
        [YamlMember(Alias = "waiting")]
        public override Models.ContainerStateWaitingV1 Waiting
        {
            get
            {
                return base.Waiting;
            }
            set
            {
                base.Waiting = value;

                __ModifiedProperties__.Add("Waiting");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
