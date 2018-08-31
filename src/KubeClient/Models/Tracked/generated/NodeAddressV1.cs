using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NodeAddress contains information for the node's address.
    /// </summary>
    public partial class NodeAddressV1 : Models.NodeAddressV1
    {
        /// <summary>
        ///     Node address type, one of Hostname, ExternalIP or InternalIP.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     The node address.
        /// </summary>
        [JsonProperty("address")]
        [YamlMember(Alias = "address")]
        public override string Address
        {
            get
            {
                return base.Address;
            }
            set
            {
                base.Address = value;

                __ModifiedProperties__.Add("Address");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
