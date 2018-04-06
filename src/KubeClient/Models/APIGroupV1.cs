using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIGroup contains the name, the supported versions, and the preferred version of a group.
    /// </summary>
    public partial class APIGroupV1
    {
        /// <summary>
        ///     preferredVersion is the version preferred by the API server, which probably is the storage version.
        /// </summary>
        [JsonProperty("preferredVersion")]
        public GroupVersionForDiscoveryV1 PreferredVersion { get; set; }

        /// <summary>
        ///     a map of client CIDR to server address that is serving this group. This is to help clients reach servers in the most network-efficient way possible. Clients can use the appropriate server address as per the CIDR that they match. In case of multiple matches, clients should use the longest matching CIDR. The server returns only those CIDRs that it thinks that the client can match. For example: the master will return an internal IP CIDR only, if the client reaches the server using an internal IP. Server looks at X-Forwarded-For header or X-Real-Ip header or request.RemoteAddr (in that order) to get the client IP.
        /// </summary>
        [JsonProperty("serverAddressByClientCIDRs", NullValueHandling = NullValueHandling.Ignore)]
        public List<ServerAddressByClientCIDRV1> ServerAddressByClientCIDRs { get; set; } = new List<ServerAddressByClientCIDRV1>();

        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        public string Kind { get; set; }

        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        ///     name is the name of the group.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     versions are the versions supported in this group.
        /// </summary>
        [JsonProperty("versions", NullValueHandling = NullValueHandling.Ignore)]
        public List<GroupVersionForDiscoveryV1> Versions { get; set; } = new List<GroupVersionForDiscoveryV1>();
    }
}
