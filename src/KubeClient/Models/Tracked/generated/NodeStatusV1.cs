using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NodeStatus is information about the current status of a node.
    /// </summary>
    public partial class NodeStatusV1 : Models.NodeStatusV1, ITracked
    {
        /// <summary>
        ///     List of volumes that are attached to the node.
        /// </summary>
        [YamlMember(Alias = "volumesAttached")]
        [JsonProperty("volumesAttached", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.AttachedVolumeV1> VolumesAttached { get; set; } = new List<Models.AttachedVolumeV1>();

        /// <summary>
        ///     Allocatable represents the resources of a node that are available for scheduling. Defaults to Capacity.
        /// </summary>
        [YamlMember(Alias = "allocatable")]
        [JsonProperty("allocatable", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Allocatable { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     NodePhase is the recently observed lifecycle phase of the node. More info: https://kubernetes.io/docs/concepts/nodes/node/#phase The field is never populated, and now is deprecated.
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public override string Phase
        {
            get
            {
                return base.Phase;
            }
            set
            {
                base.Phase = value;

                __ModifiedProperties__.Add("Phase");
            }
        }


        /// <summary>
        ///     List of attachable volumes in use (mounted) by the node.
        /// </summary>
        [YamlMember(Alias = "volumesInUse")]
        [JsonProperty("volumesInUse", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> VolumesInUse { get; set; } = new List<string>();

        /// <summary>
        ///     Set of ids/uuids to uniquely identify the node. More info: https://kubernetes.io/docs/concepts/nodes/node/#info
        /// </summary>
        [JsonProperty("nodeInfo")]
        [YamlMember(Alias = "nodeInfo")]
        public override Models.NodeSystemInfoV1 NodeInfo
        {
            get
            {
                return base.NodeInfo;
            }
            set
            {
                base.NodeInfo = value;

                __ModifiedProperties__.Add("NodeInfo");
            }
        }


        /// <summary>
        ///     List of addresses reachable to the node. Queried from cloud provider, if available. More info: https://kubernetes.io/docs/concepts/nodes/node/#addresses
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "addresses")]
        [JsonProperty("addresses", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.NodeAddressV1> Addresses { get; set; } = new List<Models.NodeAddressV1>();

        /// <summary>
        ///     Conditions is an array of current observed node conditions. More info: https://kubernetes.io/docs/concepts/nodes/node/#condition
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.NodeConditionV1> Conditions { get; set; } = new List<Models.NodeConditionV1>();

        /// <summary>
        ///     Endpoints of daemons running on the Node.
        /// </summary>
        [JsonProperty("daemonEndpoints")]
        [YamlMember(Alias = "daemonEndpoints")]
        public override Models.NodeDaemonEndpointsV1 DaemonEndpoints
        {
            get
            {
                return base.DaemonEndpoints;
            }
            set
            {
                base.DaemonEndpoints = value;

                __ModifiedProperties__.Add("DaemonEndpoints");
            }
        }


        /// <summary>
        ///     List of container images on this node
        /// </summary>
        [YamlMember(Alias = "images")]
        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ContainerImageV1> Images { get; set; } = new List<Models.ContainerImageV1>();

        /// <summary>
        ///     Capacity represents the total resources of a node. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#capacity
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Capacity { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
