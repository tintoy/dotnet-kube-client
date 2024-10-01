using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicyPort describes a port to allow traffic on
    /// </summary>
    public partial class NetworkPolicyPortV1
    {
        /// <summary>
        ///     protocol represents the protocol (TCP, UDP, or SCTP) which traffic must match. If not specified, this field defaults to TCP.
        /// </summary>
        [YamlMember(Alias = "protocol")]
        [JsonProperty("protocol", NullValueHandling = NullValueHandling.Ignore)]
        public string Protocol { get; set; }

        /// <summary>
        ///     endPort indicates that the range of ports from port to endPort if set, inclusive, should be allowed by the policy. This field cannot be defined if the port field is not defined or if the port field is defined as a named (string) port. The endPort must be equal or greater than port.
        /// </summary>
        [YamlMember(Alias = "endPort")]
        [JsonProperty("endPort", NullValueHandling = NullValueHandling.Ignore)]
        public int? EndPort { get; set; }

        /// <summary>
        ///     port represents the port on the given protocol. This can either be a numerical or named port on a pod. If this field is not provided, this matches all port names and numbers. If present, only traffic on the specified protocol AND port will be matched.
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Ignore)]
        public Int32OrStringV1 Port { get; set; }
    }
}
