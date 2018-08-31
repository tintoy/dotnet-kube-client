using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServerAddressByClientCIDR helps the client to determine the server address that they should use, depending on the clientCIDR that they match.
    /// </summary>
    public partial class ServerAddressByClientCIDRV1 : Models.ServerAddressByClientCIDRV1, ITracked
    {
        /// <summary>
        ///     The CIDR with which clients can match their IP to figure out the server address that they should use.
        /// </summary>
        [JsonProperty("clientCIDR")]
        [YamlMember(Alias = "clientCIDR")]
        public override string ClientCIDR
        {
            get
            {
                return base.ClientCIDR;
            }
            set
            {
                base.ClientCIDR = value;

                __ModifiedProperties__.Add("ClientCIDR");
            }
        }


        /// <summary>
        ///     Address of this server, suitable for a client that matches the above CIDR. This can be a hostname, hostname:port, IP or IP:port.
        /// </summary>
        [JsonProperty("serverAddress")]
        [YamlMember(Alias = "serverAddress")]
        public override string ServerAddress
        {
            get
            {
                return base.ServerAddress;
            }
            set
            {
                base.ServerAddress = value;

                __ModifiedProperties__.Add("ServerAddress");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
