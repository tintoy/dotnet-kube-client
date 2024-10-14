using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HostIP represents a single IP address allocated to the host.
    /// </summary>
    public partial class HostIPV1
    {
        /// <summary>
        ///     IP is the IP address assigned to the host
        /// </summary>
        [YamlMember(Alias = "ip")]
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Include)]
        public string Ip { get; set; }
    }
}
