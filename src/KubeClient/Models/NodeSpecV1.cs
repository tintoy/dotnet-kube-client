using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeSpec describes the attributes that a node is created with.
    /// </summary>
    [KubeResource("NodeSpec", "v1")]
    public class NodeSpecV1
    {
        /// <summary>
        ///     External ID of the node assigned by some machine database (e.g. a cloud provider). Deprecated.
        /// </summary>
        [JsonProperty("externalID")]
        public string ExternalID { get; set; }

        /// <summary>
        ///     ID of the node assigned by the cloud provider in the format: <ProviderName>://<ProviderSpecificNodeID>
        /// </summary>
        [JsonProperty("providerID")]
        public string ProviderID { get; set; }

        /// <summary>
        ///     PodCIDR represents the pod IP range assigned to the node.
        /// </summary>
        [JsonProperty("podCIDR")]
        public string PodCIDR { get; set; }

        /// <summary>
        ///     Unschedulable controls node schedulability of new pods. By default, node is schedulable. More info: https://kubernetes.io/docs/concepts/nodes/node/#manual-node-administration
        /// </summary>
        [JsonProperty("unschedulable")]
        public bool Unschedulable { get; set; }

        /// <summary>
        ///     If specified, the node's taints.
        /// </summary>
        [JsonProperty("taints", NullValueHandling = NullValueHandling.Ignore)]
        public List<TaintV1> Taints { get; set; } = new List<TaintV1>();
    }
}
