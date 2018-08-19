using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("Port")]
        public int Port { get; set; }
    }
}
