using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodIP represents a single IP address allocated to the pod.
    /// </summary>
    public partial class PodIPV1
    {
        /// <summary>
        ///     IP is the IP address assigned to the pod
        /// </summary>
        [YamlMember(Alias = "ip")]
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Include)]
        public string Ip { get; set; }
    }
}
