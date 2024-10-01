using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LifecycleHandler defines a specific action that should be taken in a lifecycle hook. One and only one of the fields, except TCPSocket must be specified.
    /// </summary>
    public partial class LifecycleHandlerV1
    {
        /// <summary>
        ///     Exec specifies the action to take.
        /// </summary>
        [YamlMember(Alias = "exec")]
        [JsonProperty("exec", NullValueHandling = NullValueHandling.Ignore)]
        public ExecActionV1 Exec { get; set; }

        /// <summary>
        ///     Sleep represents the duration that the container should sleep before being terminated.
        /// </summary>
        [YamlMember(Alias = "sleep")]
        [JsonProperty("sleep", NullValueHandling = NullValueHandling.Ignore)]
        public SleepActionV1 Sleep { get; set; }

        /// <summary>
        ///     HTTPGet specifies the http request to perform.
        /// </summary>
        [YamlMember(Alias = "httpGet")]
        [JsonProperty("httpGet", NullValueHandling = NullValueHandling.Ignore)]
        public HTTPGetActionV1 HttpGet { get; set; }

        /// <summary>
        ///     Deprecated. TCPSocket is NOT supported as a LifecycleHandler and kept for the backward compatibility. There are no validation of this field and lifecycle hooks will fail in runtime when tcp handler is specified.
        /// </summary>
        [YamlMember(Alias = "tcpSocket")]
        [JsonProperty("tcpSocket", NullValueHandling = NullValueHandling.Ignore)]
        public TCPSocketActionV1 TcpSocket { get; set; }
    }
}
