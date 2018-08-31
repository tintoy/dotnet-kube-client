using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ContainerPort represents a network port in a single container.
    /// </summary>
    public partial class ContainerPortV1 : Models.ContainerPortV1
    {
        /// <summary>
        ///     What host IP to bind the external port to.
        /// </summary>
        [JsonProperty("hostIP")]
        [YamlMember(Alias = "hostIP")]
        public override string HostIP
        {
            get
            {
                return base.HostIP;
            }
            set
            {
                base.HostIP = value;

                __ModifiedProperties__.Add("HostIP");
            }
        }


        /// <summary>
        ///     If specified, this must be an IANA_SVC_NAME and unique within the pod. Each named port in a pod must have a unique name. Name for the port that can be referred to by services.
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
        ///     Protocol for port. Must be UDP or TCP. Defaults to "TCP".
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
        ///     Number of port to expose on the pod's IP address. This must be a valid port number, 0 &lt; x &lt; 65536.
        /// </summary>
        [JsonProperty("containerPort")]
        [YamlMember(Alias = "containerPort")]
        public override int ContainerPort
        {
            get
            {
                return base.ContainerPort;
            }
            set
            {
                base.ContainerPort = value;

                __ModifiedProperties__.Add("ContainerPort");
            }
        }


        /// <summary>
        ///     Number of port to expose on the host. If specified, this must be a valid port number, 0 &lt; x &lt; 65536. If HostNetwork is specified, this must match ContainerPort. Most containers do not need this.
        /// </summary>
        [JsonProperty("hostPort")]
        [YamlMember(Alias = "hostPort")]
        public override int HostPort
        {
            get
            {
                return base.HostPort;
            }
            set
            {
                base.HostPort = value;

                __ModifiedProperties__.Add("HostPort");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
