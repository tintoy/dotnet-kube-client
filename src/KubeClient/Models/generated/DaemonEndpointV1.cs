using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonEndpoint contains information about a single Daemon endpoint.
    /// </summary>
    public partial class DaemonEndpointV1
    {
        /// <summary>
        ///     Port number of the given endpoint.
        /// </summary>
        [YamlMember(Alias = "Port")]
        [JsonProperty("Port", NullValueHandling = NullValueHandling.Include)]
        public int Port { get; set; }
    }
}
