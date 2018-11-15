using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerPort represents a network port in a single container.
    /// </summary>
    public partial class ContainerPortV1
    {
        /// <summary>
        ///     What host IP to bind the external port to.
        /// </summary>
        [YamlMember(Alias = "hostIP")]
        [JsonProperty("hostIP", NullValueHandling = NullValueHandling.Ignore)]
        public string HostIP { get; set; }

        /// <summary>
        ///     If specified, this must be an IANA_SVC_NAME and unique within the pod. Each named port in a pod must have a unique name. Name for the port that can be referred to by services.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Protocol for port. Must be UDP or TCP. Defaults to "TCP".
        /// </summary>
        [YamlMember(Alias = "protocol")]
        [JsonProperty("protocol", NullValueHandling = NullValueHandling.Ignore)]
        public string Protocol { get; set; }

        /// <summary>
        ///     Number of port to expose on the pod's IP address. This must be a valid port number, 0 &lt; x &lt; 65536.
        /// </summary>
        [YamlMember(Alias = "containerPort")]
        [JsonProperty("containerPort", NullValueHandling = NullValueHandling.Include)]
        public int ContainerPort { get; set; }

        /// <summary>
        ///     Number of port to expose on the host. If specified, this must be a valid port number, 0 &lt; x &lt; 65536. If HostNetwork is specified, this must match ContainerPort. Most containers do not need this.
        /// </summary>
        [YamlMember(Alias = "hostPort")]
        [JsonProperty("hostPort", NullValueHandling = NullValueHandling.Ignore)]
        public int? HostPort { get; set; }
    }
}
