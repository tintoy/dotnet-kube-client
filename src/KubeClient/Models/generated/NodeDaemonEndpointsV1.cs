using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

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
        [YamlMember(Alias = "kubeletEndpoint")]
        public virtual DaemonEndpointV1 KubeletEndpoint { get; set; }
    }
}
