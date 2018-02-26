using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeObject("NetworkPolicyPort", "v1beta1")]
    public class NetworkPolicyPortV1Beta1
    {
        /// <summary>
        ///     Optional.  The protocol (TCP or UDP) which traffic must match. If not specified, this field defaults to TCP.
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        ///     If specified, the port on the given protocol.  This can either be a numerical or named port on a pod.  If this field is not provided, this matches all port names and numbers. If present, only traffic on the specified protocol AND port will be matched.
        /// </summary>
        [JsonProperty("port")]
        public string Port { get; set; }
    }
}
