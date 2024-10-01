using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerUser represents user identity information
    /// </summary>
    public partial class ContainerUserV1
    {
        /// <summary>
        ///     Linux holds user identity information initially attached to the first process of the containers in Linux. Note that the actual running identity can be changed if the process has enough privilege to do so.
        /// </summary>
        [YamlMember(Alias = "linux")]
        [JsonProperty("linux", NullValueHandling = NullValueHandling.Ignore)]
        public LinuxContainerUserV1 Linux { get; set; }
    }
}
