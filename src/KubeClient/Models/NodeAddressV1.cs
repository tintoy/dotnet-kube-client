using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeAddress contains information for the node's address.
    /// </summary>
    public partial class NodeAddressV1
    {
        /// <summary>
        ///     The node address.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        ///     Node address type, one of Hostname, ExternalIP or InternalIP.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
