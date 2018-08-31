using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class NetworkPolicyPortV1Beta1 : Models.NetworkPolicyPortV1Beta1, ITracked
    {
        /// <summary>
        ///     Optional.  The protocol (TCP or UDP) which traffic must match. If not specified, this field defaults to TCP.
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
        ///     If specified, the port on the given protocol.  This can either be a numerical or named port on a pod.  If this field is not provided, this matches all port names and numbers. If present, only traffic on the specified protocol AND port will be matched.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public override string Port
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
