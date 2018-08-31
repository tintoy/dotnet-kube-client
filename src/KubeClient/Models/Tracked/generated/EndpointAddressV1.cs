using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EndpointAddress is a tuple that describes single IP address.
    /// </summary>
    public partial class EndpointAddressV1 : Models.EndpointAddressV1, ITracked
    {
        /// <summary>
        ///     The Hostname of this endpoint
        /// </summary>
        [JsonProperty("hostname")]
        [YamlMember(Alias = "hostname")]
        public override string Hostname
        {
            get
            {
                return base.Hostname;
            }
            set
            {
                base.Hostname = value;

                __ModifiedProperties__.Add("Hostname");
            }
        }


        /// <summary>
        ///     Optional: Node hosting this endpoint. This can be used to determine endpoints local to a node.
        /// </summary>
        [JsonProperty("nodeName")]
        [YamlMember(Alias = "nodeName")]
        public override string NodeName
        {
            get
            {
                return base.NodeName;
            }
            set
            {
                base.NodeName = value;

                __ModifiedProperties__.Add("NodeName");
            }
        }


        /// <summary>
        ///     Reference to object providing the endpoint.
        /// </summary>
        [JsonProperty("targetRef")]
        [YamlMember(Alias = "targetRef")]
        public override Models.ObjectReferenceV1 TargetRef
        {
            get
            {
                return base.TargetRef;
            }
            set
            {
                base.TargetRef = value;

                __ModifiedProperties__.Add("TargetRef");
            }
        }


        /// <summary>
        ///     The IP of this endpoint. May not be loopback (127.0.0.0/8), link-local (169.254.0.0/16), or link-local multicast ((224.0.0.0/24). IPv6 is also accepted but not fully supported on all platforms. Also, certain kubernetes components, like kube-proxy, are not IPv6 ready.
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
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
