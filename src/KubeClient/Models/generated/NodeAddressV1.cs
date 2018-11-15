using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeAddress contains information for the node's address.
    /// </summary>
    public partial class NodeAddressV1
    {
        /// <summary>
        ///     Node address type, one of Hostname, ExternalIP or InternalIP.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     The node address.
        /// </summary>
        [YamlMember(Alias = "address")]
        [JsonProperty("address", NullValueHandling = NullValueHandling.Include)]
        public string Address { get; set; }
    }
}
