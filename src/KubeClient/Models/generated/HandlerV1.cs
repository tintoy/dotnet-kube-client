using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Handler defines a specific action that should be taken
    /// </summary>
    public partial class HandlerV1
    {
        /// <summary>
        ///     One and only one of the following should be specified. Exec specifies the action to take.
        /// </summary>
        [YamlMember(Alias = "exec")]
        [JsonProperty("exec", NullValueHandling = NullValueHandling.Ignore)]
        public ExecActionV1 Exec { get; set; }

        /// <summary>
        ///     HTTPGet specifies the http request to perform.
        /// </summary>
        [YamlMember(Alias = "httpGet")]
        [JsonProperty("httpGet", NullValueHandling = NullValueHandling.Ignore)]
        public HTTPGetActionV1 HttpGet { get; set; }

        /// <summary>
        ///     TCPSocket specifies an action involving a TCP port. TCP hooks not yet supported
        /// </summary>
        [YamlMember(Alias = "tcpSocket")]
        [JsonProperty("tcpSocket", NullValueHandling = NullValueHandling.Ignore)]
        public TCPSocketActionV1 TcpSocket { get; set; }
    }
}
