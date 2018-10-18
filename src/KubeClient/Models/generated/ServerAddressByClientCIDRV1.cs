using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServerAddressByClientCIDR helps the client to determine the server address that they should use, depending on the clientCIDR that they match.
    /// </summary>
    public partial class ServerAddressByClientCIDRV1
    {
        /// <summary>
        ///     The CIDR with which clients can match their IP to figure out the server address that they should use.
        /// </summary>
        [YamlMember(Alias = "clientCIDR")]
        [JsonProperty("clientCIDR", NullValueHandling = NullValueHandling.Include)]
        public string ClientCIDR { get; set; }

        /// <summary>
        ///     Address of this server, suitable for a client that matches the above CIDR. This can be a hostname, hostname:port, IP or IP:port.
        /// </summary>
        [YamlMember(Alias = "serverAddress")]
        [JsonProperty("serverAddress", NullValueHandling = NullValueHandling.Include)]
        public string ServerAddress { get; set; }
    }
}
