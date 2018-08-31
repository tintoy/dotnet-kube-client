using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     HostAlias holds the mapping between IP and hostnames that will be injected as an entry in the pod's hosts file.
    /// </summary>
    public partial class HostAliasV1 : Models.HostAliasV1, ITracked
    {
        /// <summary>
        ///     IP address of the host file entry.
        /// </summary>
        [JsonProperty("ip")]
        [YamlMember(Alias = "ip")]
        public override string Ip
        {
            get
            {
                return base.Ip;
            }
            set
            {
                base.Ip = value;

                __ModifiedProperties__.Add("Ip");
            }
        }


        /// <summary>
        ///     Hostnames for the above IP address.
        /// </summary>
        [YamlMember(Alias = "hostnames")]
        [JsonProperty("hostnames", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Hostnames { get; set; } = new List<string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
