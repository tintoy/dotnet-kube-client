using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIGroup contains the name, the supported versions, and the preferred version of a group.
    /// </summary>
    public partial class APIGroupV1 : KubeObjectV1
    {
        /// <summary>
        ///     name is the name of the group.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     preferredVersion is the version preferred by the API server, which probably is the storage version.
        /// </summary>
        [YamlMember(Alias = "preferredVersion")]
        [JsonProperty("preferredVersion", NullValueHandling = NullValueHandling.Ignore)]
        public GroupVersionForDiscoveryV1 PreferredVersion { get; set; }

        /// <summary>
        ///     a map of client CIDR to server address that is serving this group. This is to help clients reach servers in the most network-efficient way possible. Clients can use the appropriate server address as per the CIDR that they match. In case of multiple matches, clients should use the longest matching CIDR. The server returns only those CIDRs that it thinks that the client can match. For example: the master will return an internal IP CIDR only, if the client reaches the server using an internal IP. Server looks at X-Forwarded-For header or X-Real-Ip header or request.RemoteAddr (in that order) to get the client IP.
        /// </summary>
        [YamlMember(Alias = "serverAddressByClientCIDRs")]
        [JsonProperty("serverAddressByClientCIDRs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ServerAddressByClientCIDRV1> ServerAddressByClientCIDRs { get; } = new List<ServerAddressByClientCIDRV1>();

        /// <summary>
        ///     Determine whether the <see cref="ServerAddressByClientCIDRs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeServerAddressByClientCIDRs() => ServerAddressByClientCIDRs.Count > 0;

        /// <summary>
        ///     versions are the versions supported in this group.
        /// </summary>
        [YamlMember(Alias = "versions")]
        [JsonProperty("versions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<GroupVersionForDiscoveryV1> Versions { get; } = new List<GroupVersionForDiscoveryV1>();
    }
}
