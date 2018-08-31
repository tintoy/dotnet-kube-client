using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EndpointPort is a tuple that describes a single port.
    /// </summary>
    public partial class EndpointPortV1 : Models.EndpointPortV1, ITracked
    {
        /// <summary>
        ///     The name of this port (corresponds to ServicePort.Name). Must be a DNS_LABEL. Optional only if one port is defined.
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
        ///     The IP protocol for this port. Must be UDP or TCP. Default is TCP.
        /// </summary>
        [JsonProperty("protocol")]
        [YamlMember(Alias = "protocol")]
        public override string Protocol
        {
            get
            {
                return base.Protocol;
            }
            set
            {
                base.Protocol = value;

                __ModifiedProperties__.Add("Protocol");
            }
        }


        /// <summary>
        ///     The port number of the endpoint.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public override int Port
        {
            get
            {
                return base.Port;
            }
            set
            {
                base.Port = value;

                __ModifiedProperties__.Add("Port");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
