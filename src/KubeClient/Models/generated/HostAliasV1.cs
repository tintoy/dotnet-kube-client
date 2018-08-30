using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HostAlias holds the mapping between IP and hostnames that will be injected as an entry in the pod's hosts file.
    /// </summary>
    public partial class HostAliasV1
    {
        /// <summary>
        ///     IP address of the host file entry.
        /// </summary>
        [JsonProperty("ip")]
        [YamlMember(Alias = "ip")]
        public virtual string Ip { get; set; }

        /// <summary>
        ///     Hostnames for the above IP address.
        /// </summary>
        [YamlMember(Alias = "hostnames")]
        [JsonProperty("hostnames", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Hostnames { get; set; } = new List<string>();
    }
}
