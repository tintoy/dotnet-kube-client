using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     TCPSocketAction describes an action based on opening a socket
    /// </summary>
    public partial class TCPSocketActionV1 : Models.TCPSocketActionV1, ITracked
    {
        /// <summary>
        ///     Optional: Host name to connect to, defaults to the pod IP.
        /// </summary>
        [JsonProperty("host")]
        [YamlMember(Alias = "host")]
        public override string Host
        {
            get
            {
                return base.Host;
            }
            set
            {
                base.Host = value;

                __ModifiedProperties__.Add("Host");
            }
        }


        /// <summary>
        ///     Number or name of the port to access on the container. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME.
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
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
