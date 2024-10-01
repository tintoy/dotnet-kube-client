using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressPortStatus represents the error condition of a service port
    /// </summary>
    public partial class IngressPortStatusV1
    {
        /// <summary>
        ///     protocol is the protocol of the ingress port. The supported values are: "TCP", "UDP", "SCTP"
        /// </summary>
        [YamlMember(Alias = "protocol")]
        [JsonProperty("protocol", NullValueHandling = NullValueHandling.Include)]
        public string Protocol { get; set; }

        /// <summary>
        ///     error is to record the problem with the service port The format of the error shall comply with the following rules: - built-in error values shall be specified in this file and those shall use
        ///       CamelCase names
        ///     - cloud provider specific error values must have names that comply with the
        ///       format foo.example.com/CamelCase.
        /// </summary>
        [YamlMember(Alias = "error")]
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        ///     port is the port number of the ingress port.
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Include)]
        public int Port { get; set; }
    }
}
