using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicyPort describes a port to allow traffic on
    /// </summary>
    public partial class NetworkPolicyPortV1
    {
        /// <summary>
        ///     The protocol (TCP or UDP) which traffic must match. If not specified, this field defaults to TCP.
        /// </summary>
        [JsonProperty("protocol")]
        [YamlMember(Alias = "protocol")]
        public virtual string Protocol { get; set; }

        /// <summary>
        ///     The port on the given protocol. This can either be a numerical or named port on a pod. If this field is not provided, this matches all port names and numbers.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public virtual string Port { get; set; }
    }
}
