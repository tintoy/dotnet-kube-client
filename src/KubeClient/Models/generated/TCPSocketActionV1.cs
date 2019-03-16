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
        [YamlMember(Alias = "host")]
        [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
        public string Host { get; set; }

        /// <summary>
        ///     Number or name of the port to access on the container. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME.
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Include)]
        public Int32OrStringV1 Port { get; set; }
    }
}
