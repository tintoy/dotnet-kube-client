using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NodeDaemonEndpoints lists ports opened by daemons running on the Node.
    /// </summary>
    public partial class NodeDaemonEndpointsV1 : Models.NodeDaemonEndpointsV1, ITracked
    {
        /// <summary>
        ///     Endpoint on which Kubelet is listening.
        /// </summary>
        [JsonProperty("kubeletEndpoint")]
        [YamlMember(Alias = "kubeletEndpoint")]
        public override Models.DaemonEndpointV1 KubeletEndpoint
        {
            get
            {
                return base.KubeletEndpoint;
            }
            set
            {
                base.KubeletEndpoint = value;

                __ModifiedProperties__.Add("KubeletEndpoint");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
