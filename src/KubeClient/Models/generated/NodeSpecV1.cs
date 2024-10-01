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
        ///     Deprecated. Not all kubelets will set this field. Remove field after 1.13. see: https://issues.k8s.io/61966
        /// </summary>
        [YamlMember(Alias = "externalID")]
        [JsonProperty("externalID", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalID { get; set; }

        /// <summary>
        ///     ID of the node assigned by the cloud provider in the format: &lt;ProviderName&gt;://&lt;ProviderSpecificNodeID&gt;
        /// </summary>
        [YamlMember(Alias = "providerID")]
        [JsonProperty("providerID", NullValueHandling = NullValueHandling.Ignore)]
        public string ProviderID { get; set; }

        /// <summary>
        ///     PodCIDR represents the pod IP range assigned to the node.
        /// </summary>
        [YamlMember(Alias = "podCIDR")]
        [JsonProperty("podCIDR", NullValueHandling = NullValueHandling.Ignore)]
        public string PodCIDR { get; set; }

        /// <summary>
        ///     Deprecated: Previously used to specify the source of the node's configuration for the DynamicKubeletConfig feature. This feature is removed.
        /// </summary>
        [YamlMember(Alias = "configSource")]
        [JsonProperty("configSource", NullValueHandling = NullValueHandling.Ignore)]
        public NodeConfigSourceV1 ConfigSource { get; set; }

        /// <summary>
        ///     Unschedulable controls node schedulability of new pods. By default, node is schedulable. More info: https://kubernetes.io/docs/concepts/nodes/node/#manual-node-administration
        /// </summary>
        [YamlMember(Alias = "unschedulable")]
        [JsonProperty("unschedulable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Unschedulable { get; set; }

        /// <summary>
        ///     podCIDRs represents the IP ranges assigned to the node for usage by Pods on that node. If this field is specified, the 0th entry must match the podCIDR field. It may contain at most 1 value for each of IPv4 and IPv6.
        /// </summary>
        [MergeStrategy]
        [YamlMember(Alias = "podCIDRs")]
        [JsonProperty("podCIDRs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> PodCIDRs { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="PodCIDRs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePodCIDRs() => PodCIDRs.Count > 0;

        /// <summary>
        ///     If specified, the node's taints.
        /// </summary>
        [YamlMember(Alias = "taints")]
        [JsonProperty("taints", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<TaintV1> Taints { get; } = new List<TaintV1>();

        /// <summary>
        ///     Determine whether the <see cref="Taints"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTaints() => Taints.Count > 0;
    }
}
