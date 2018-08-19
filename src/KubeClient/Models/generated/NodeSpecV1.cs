using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeSpec describes the attributes that a node is created with.
    /// </summary>
    public partial class NodeSpecV1
    {
        /// <summary>
        ///     External ID of the node assigned by some machine database (e.g. a cloud provider). Deprecated.
        /// </summary>
        [JsonProperty("externalID")]
        [YamlMember(Alias = "externalID")]
        public string ExternalID { get; set; }

        /// <summary>
        ///     ID of the node assigned by the cloud provider in the format: &lt;ProviderName&gt;://&lt;ProviderSpecificNodeID&gt;
        /// </summary>
        [JsonProperty("providerID")]
        [YamlMember(Alias = "providerID")]
        public string ProviderID { get; set; }

        /// <summary>
        ///     PodCIDR represents the pod IP range assigned to the node.
        /// </summary>
        [JsonProperty("podCIDR")]
        [YamlMember(Alias = "podCIDR")]
        public string PodCIDR { get; set; }

        /// <summary>
        ///     Unschedulable controls node schedulability of new pods. By default, node is schedulable. More info: https://kubernetes.io/docs/concepts/nodes/node/#manual-node-administration
        /// </summary>
        [JsonProperty("unschedulable")]
        [YamlMember(Alias = "unschedulable")]
        public bool Unschedulable { get; set; }

        /// <summary>
        ///     If specified, the node's taints.
        /// </summary>
        [YamlMember(Alias = "taints")]
        [JsonProperty("taints", NullValueHandling = NullValueHandling.Ignore)]
        public List<TaintV1> Taints { get; set; } = new List<TaintV1>();
    }
}
