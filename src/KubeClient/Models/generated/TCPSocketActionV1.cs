using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TCPSocketAction describes an action based on opening a socket
    /// </summary>
    public partial class TCPSocketActionV1
    {
        /// <summary>
        ///     Optional: Host name to connect to, defaults to the pod IP.
        /// </summary>
        [JsonProperty("host")]
        [YamlMember(Alias = "host")]
        public string Host { get; set; }

        /// <summary>
        ///     Number or name of the port to access on the container. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public string Port { get; set; }
    }
}
