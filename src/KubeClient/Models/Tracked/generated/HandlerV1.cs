using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Handler defines a specific action that should be taken
    /// </summary>
    public partial class HandlerV1 : Models.HandlerV1, ITracked
    {
        /// <summary>
        ///     One and only one of the following should be specified. Exec specifies the action to take.
        /// </summary>
        [JsonProperty("exec")]
        [YamlMember(Alias = "exec")]
        public override Models.ExecActionV1 Exec
        {
            get
            {
                return base.Exec;
            }
            set
            {
                base.Exec = value;

                __ModifiedProperties__.Add("Exec");
            }
        }


        /// <summary>
        ///     HTTPGet specifies the http request to perform.
        /// </summary>
        [JsonProperty("httpGet")]
        [YamlMember(Alias = "httpGet")]
        public override Models.HTTPGetActionV1 HttpGet
        {
            get
            {
                return base.HttpGet;
            }
            set
            {
                base.HttpGet = value;

                __ModifiedProperties__.Add("HttpGet");
            }
        }


        /// <summary>
        ///     TCPSocket specifies an action involving a TCP port. TCP hooks not yet supported
        /// </summary>
        [JsonProperty("tcpSocket")]
        [YamlMember(Alias = "tcpSocket")]
        public override Models.TCPSocketActionV1 TcpSocket
        {
            get
            {
                return base.TcpSocket;
            }
            set
            {
                base.TcpSocket = value;

                __ModifiedProperties__.Add("TcpSocket");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
