using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerState holds a possible state of container. Only one of its members may be specified. If none of them is specified, the default one is ContainerStateWaiting.
    /// </summary>
    public partial class ContainerStateV1
    {
        /// <summary>
        ///     Details about a terminated container
        /// </summary>
        [YamlMember(Alias = "terminated")]
        [JsonProperty("terminated", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateTerminatedV1 Terminated { get; set; }

        /// <summary>
        ///     Details about a running container
        /// </summary>
        [YamlMember(Alias = "running")]
        [JsonProperty("running", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateRunningV1 Running { get; set; }

        /// <summary>
        ///     Details about a waiting container
        /// </summary>
        [YamlMember(Alias = "waiting")]
        [JsonProperty("waiting", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateWaitingV1 Waiting { get; set; }
    }
}
