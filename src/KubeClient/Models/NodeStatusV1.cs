using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeStatus is information about the current status of a node.
    /// </summary>
    public partial class NodeStatusV1
    {
        /// <summary>
        ///     Conditions is an array of current observed node conditions. More info: https://kubernetes.io/docs/concepts/nodes/node/#condition
        /// </summary>
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeConditionV1> Conditions { get; set; } = new List<NodeConditionV1>();

        /// <summary>
        ///     List of attachable volumes in use (mounted) by the node.
        /// </summary>
        [JsonProperty("volumesInUse", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> VolumesInUse { get; set; } = new List<string>();

        /// <summary>
        ///     List of volumes that are attached to the node.
        /// </summary>
        [JsonProperty("volumesAttached", NullValueHandling = NullValueHandling.Ignore)]
        public List<AttachedVolumeV1> VolumesAttached { get; set; } = new List<AttachedVolumeV1>();

        /// <summary>
        ///     List of addresses reachable to the node. Queried from cloud provider, if available. More info: https://kubernetes.io/docs/concepts/nodes/node/#addresses
        /// </summary>
        [JsonProperty("addresses", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeAddressV1> Addresses { get; set; } = new List<NodeAddressV1>();

        /// <summary>
        ///     Endpoints of daemons running on the Node.
        /// </summary>
        [JsonProperty("daemonEndpoints")]
        public NodeDaemonEndpointsV1 DaemonEndpoints { get; set; }

        /// <summary>
        ///     NodePhase is the recently observed lifecycle phase of the node. More info: https://kubernetes.io/docs/concepts/nodes/node/#phase The field is never populated, and now is deprecated.
        /// </summary>
        [JsonProperty("phase")]
        public string Phase { get; set; }

        /// <summary>
        ///     List of container images on this node
        /// </summary>
        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public List<ContainerImageV1> Images { get; set; } = new List<ContainerImageV1>();

        /// <summary>
        ///     Capacity represents the total resources of a node. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#capacity
        /// </summary>
        [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Capacity { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Allocatable represents the resources of a node that are available for scheduling. Defaults to Capacity.
        /// </summary>
        [JsonProperty("allocatable", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Allocatable { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Set of ids/uuids to uniquely identify the node. More info: https://kubernetes.io/docs/concepts/nodes/node/#info
        /// </summary>
        [JsonProperty("nodeInfo")]
        public NodeSystemInfoV1 NodeInfo { get; set; }
    }
}
