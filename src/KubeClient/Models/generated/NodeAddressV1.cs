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
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }

        /// <summary>
        ///     The node address.
        /// </summary>
        [JsonProperty("address")]
        [YamlMember(Alias = "address")]
        public virtual string Address { get; set; }
    }
}
