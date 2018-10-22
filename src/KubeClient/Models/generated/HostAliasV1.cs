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
        [YamlMember(Alias = "ip")]
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        /// <summary>
        ///     Hostnames for the above IP address.
        /// </summary>
        [YamlMember(Alias = "hostnames")]
        [JsonProperty("hostnames", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Hostnames { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Hostnames"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHostnames() => Hostnames.Count > 0;
    }
}
