using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStateWaiting is a waiting state of a container.
    /// </summary>
    public partial class ContainerStateWaitingV1
    {
        /// <summary>
        ///     Message regarding why the container is not yet running.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public virtual string Message { get; set; }

        /// <summary>
        ///     (brief) reason the container is not yet running.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public virtual string Reason { get; set; }
    }
}
