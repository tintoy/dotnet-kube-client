using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerStatus contains details for the current status of this container.
    /// </summary>
    public partial class ContainerStatusV1
    {
        /// <summary>
        ///     Container's ID in the format 'docker://&lt;container_id&gt;'.
        /// </summary>
        [YamlMember(Alias = "containerID")]
        [JsonProperty("containerID", NullValueHandling = NullValueHandling.Ignore)]
        public string ContainerID { get; set; }

        /// <summary>
        ///     ImageID of the container's image.
        /// </summary>
        [YamlMember(Alias = "imageID")]
        [JsonProperty("imageID", NullValueHandling = NullValueHandling.Include)]
        public string ImageID { get; set; }

        /// <summary>
        ///     The image the container is running. More info: https://kubernetes.io/docs/concepts/containers/images
        /// </summary>
        [YamlMember(Alias = "image")]
        [JsonProperty("image", NullValueHandling = NullValueHandling.Include)]
        public string Image { get; set; }

        /// <summary>
        ///     Details about the container's last termination condition.
        /// </summary>
        [YamlMember(Alias = "lastState")]
        [JsonProperty("lastState", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateV1 LastState { get; set; }

        /// <summary>
        ///     This must be a DNS_LABEL. Each container in a pod must have a unique name. Cannot be updated.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Details about the container's current condition.
        /// </summary>
        [YamlMember(Alias = "state")]
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerStateV1 State { get; set; }

        /// <summary>
        ///     The number of times the container has been restarted, currently based on the number of dead containers that have not yet been removed. Note that this is calculated from dead containers. But those containers are subject to garbage collection. This value will get capped at 5 by GC.
        /// </summary>
        [YamlMember(Alias = "restartCount")]
        [JsonProperty("restartCount", NullValueHandling = NullValueHandling.Include)]
        public int RestartCount { get; set; }

        /// <summary>
        ///     Specifies whether the container has passed its readiness probe.
        /// </summary>
        [YamlMember(Alias = "ready")]
        [JsonProperty("ready", NullValueHandling = NullValueHandling.Include)]
        public bool Ready { get; set; }
    }
}
