using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     EndpointAddress is a tuple that describes single IP address.
    /// </summary>
    public partial class EndpointAddressV1
    {
        /// <summary>
        ///     The Hostname of this endpoint
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        ///     Optional: Node hosting this endpoint. This can be used to determine endpoints local to a node.
        /// </summary>
        [JsonProperty("nodeName")]
        public string NodeName { get; set; }

        /// <summary>
        ///     Reference to object providing the endpoint.
        /// </summary>
        [JsonProperty("targetRef")]
        public ObjectReferenceV1 TargetRef { get; set; }

        /// <summary>
        ///     The IP of this endpoint. May not be loopback (127.0.0.0/8), link-local (169.254.0.0/16), or link-local multicast ((224.0.0.0/24). IPv6 is also accepted but not fully supported on all platforms. Also, certain kubernetes components, like kube-proxy, are not IPv6 ready.
        /// </summary>
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}
