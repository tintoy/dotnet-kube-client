using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     APIGroup contains the name, the supported versions, and the preferred version of a group.
    /// </summary>
    public partial class APIGroupV1 : Models.APIGroupV1, ITracked
    {
        /// <summary>
        ///     Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     name is the name of the group.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#resources
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public override string ApiVersion
        {
            get
            {
                return base.ApiVersion;
            }
            set
            {
                base.ApiVersion = value;

                __ModifiedProperties__.Add("ApiVersion");
            }
        }


        /// <summary>
        ///     preferredVersion is the version preferred by the API server, which probably is the storage version.
        /// </summary>
        [JsonProperty("preferredVersion")]
        [YamlMember(Alias = "preferredVersion")]
        public override Models.GroupVersionForDiscoveryV1 PreferredVersion
        {
            get
            {
                return base.PreferredVersion;
            }
            set
            {
                base.PreferredVersion = value;

                __ModifiedProperties__.Add("PreferredVersion");
            }
        }


        /// <summary>
        ///     a map of client CIDR to server address that is serving this group. This is to help clients reach servers in the most network-efficient way possible. Clients can use the appropriate server address as per the CIDR that they match. In case of multiple matches, clients should use the longest matching CIDR. The server returns only those CIDRs that it thinks that the client can match. For example: the master will return an internal IP CIDR only, if the client reaches the server using an internal IP. Server looks at X-Forwarded-For header or X-Real-Ip header or request.RemoteAddr (in that order) to get the client IP.
        /// </summary>
        [YamlMember(Alias = "serverAddressByClientCIDRs")]
        [JsonProperty("serverAddressByClientCIDRs", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ServerAddressByClientCIDRV1> ServerAddressByClientCIDRs { get; set; } = new List<Models.ServerAddressByClientCIDRV1>();

        /// <summary>
        ///     versions are the versions supported in this group.
        /// </summary>
        [YamlMember(Alias = "versions")]
        [JsonProperty("versions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.GroupVersionForDiscoveryV1> Versions { get; set; } = new List<Models.GroupVersionForDiscoveryV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
