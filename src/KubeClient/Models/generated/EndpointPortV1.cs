using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EndpointPort is a tuple that describes a single port.
    /// </summary>
    public partial class EndpointPortV1
    {
        /// <summary>
        ///     The name of this port (corresponds to ServicePort.Name). Must be a DNS_LABEL. Optional only if one port is defined.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     The IP protocol for this port. Must be UDP or TCP. Default is TCP.
        /// </summary>
        [JsonProperty("protocol")]
        [YamlMember(Alias = "protocol")]
        public virtual string Protocol { get; set; }

        /// <summary>
        ///     The port number of the endpoint.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public virtual int Port { get; set; }
    }
}
