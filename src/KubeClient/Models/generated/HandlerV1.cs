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
        ///     TCPSocket specifies an action involving a TCP port. TCP hooks not yet supported
        /// </summary>
        [JsonProperty("tcpSocket")]
        [YamlMember(Alias = "tcpSocket")]
        public TCPSocketActionV1 TcpSocket { get; set; }

        /// <summary>
        ///     HTTPGet specifies the http request to perform.
        /// </summary>
        [JsonProperty("httpGet")]
        [YamlMember(Alias = "httpGet")]
        public HTTPGetActionV1 HttpGet { get; set; }

        /// <summary>
        ///     One and only one of the following should be specified. Exec specifies the action to take.
        /// </summary>
        [JsonProperty("exec")]
        [YamlMember(Alias = "exec")]
        public ExecActionV1 Exec { get; set; }
    }
}
