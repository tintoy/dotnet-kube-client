using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class PortStatusV1
    {
        /// <summary>
        ///     Protocol is the protocol of the service port of which status is recorded here The supported values are: "TCP", "UDP", "SCTP"
        /// </summary>
        [YamlMember(Alias = "protocol")]
        [JsonProperty("protocol", NullValueHandling = NullValueHandling.Include)]
        public string Protocol { get; set; }

        /// <summary>
        ///     Error is to record the problem with the service port The format of the error shall comply with the following rules: - built-in error values shall be specified in this file and those shall use
        ///       CamelCase names
        ///     - cloud provider specific error values must have names that comply with the
        ///       format foo.example.com/CamelCase.
        /// </summary>
        [YamlMember(Alias = "error")]
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        ///     Port is the port number of the service port of which status is recorded here
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Include)]
        public int Port { get; set; }
    }
}
