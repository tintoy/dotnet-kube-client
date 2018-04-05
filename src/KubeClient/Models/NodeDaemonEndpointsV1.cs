using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeDaemonEndpoints lists ports opened by daemons running on the Node.
    /// </summary>
    public partial class NodeDaemonEndpointsV1
    {
        /// <summary>
        ///     Endpoint on which Kubelet is listening.
        /// </summary>
        [JsonProperty("kubeletEndpoint")]
        public DaemonEndpointV1 KubeletEndpoint { get; set; }
    }
}
