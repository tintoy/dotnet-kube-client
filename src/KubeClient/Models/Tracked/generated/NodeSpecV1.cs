using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NodeSpec describes the attributes that a node is created with.
    /// </summary>
    public partial class NodeSpecV1 : Models.NodeSpecV1, ITracked
    {
        /// <summary>
        ///     External ID of the node assigned by some machine database (e.g. a cloud provider). Deprecated.
        /// </summary>
        [JsonProperty("externalID")]
        [YamlMember(Alias = "externalID")]
        public override string ExternalID
        {
            get
            {
                return base.ExternalID;
            }
            set
            {
                base.ExternalID = value;

                __ModifiedProperties__.Add("ExternalID");
            }
        }


        /// <summary>
        ///     ID of the node assigned by the cloud provider in the format: &lt;ProviderName&gt;://&lt;ProviderSpecificNodeID&gt;
        /// </summary>
        [JsonProperty("providerID")]
        [YamlMember(Alias = "providerID")]
        public override string ProviderID
        {
            get
            {
                return base.ProviderID;
            }
            set
            {
                base.ProviderID = value;

                __ModifiedProperties__.Add("ProviderID");
            }
        }


        /// <summary>
        ///     PodCIDR represents the pod IP range assigned to the node.
        /// </summary>
        [JsonProperty("podCIDR")]
        [YamlMember(Alias = "podCIDR")]
        public override string PodCIDR
        {
            get
            {
                return base.PodCIDR;
            }
            set
            {
                base.PodCIDR = value;

                __ModifiedProperties__.Add("PodCIDR");
            }
        }


        /// <summary>
        ///     Unschedulable controls node schedulability of new pods. By default, node is schedulable. More info: https://kubernetes.io/docs/concepts/nodes/node/#manual-node-administration
        /// </summary>
        [JsonProperty("unschedulable")]
        [YamlMember(Alias = "unschedulable")]
        public override bool Unschedulable
        {
            get
            {
                return base.Unschedulable;
            }
            set
            {
                base.Unschedulable = value;

                __ModifiedProperties__.Add("Unschedulable");
            }
        }


        /// <summary>
        ///     If specified, the node's taints.
        /// </summary>
        [YamlMember(Alias = "taints")]
        [JsonProperty("taints", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.TaintV1> Taints { get; set; } = new List<Models.TaintV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
