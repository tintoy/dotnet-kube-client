using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeStatus is information about the current status of a node.
    /// </summary>
    public partial class NodeStatusV1
    {
        /// <summary>
        ///     List of volumes that are attached to the node.
        /// </summary>
        [YamlMember(Alias = "volumesAttached")]
        [JsonProperty("volumesAttached", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<AttachedVolumeV1> VolumesAttached { get; } = new List<AttachedVolumeV1>();

        /// <summary>
        ///     Determine whether the <see cref="VolumesAttached"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumesAttached() => VolumesAttached.Count > 0;

        /// <summary>
        ///     Allocatable represents the resources of a node that are available for scheduling. Defaults to Capacity.
        /// </summary>
        [YamlMember(Alias = "allocatable")]
        [JsonProperty("allocatable", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Allocatable { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Allocatable"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllocatable() => Allocatable.Count > 0;

        /// <summary>
        ///     NodePhase is the recently observed lifecycle phase of the node. More info: https://kubernetes.io/docs/concepts/nodes/node/#phase The field is never populated, and now is deprecated.
        /// </summary>
        [YamlMember(Alias = "phase")]
        [JsonProperty("phase", NullValueHandling = NullValueHandling.Ignore)]
        public string Phase { get; set; }

        /// <summary>
        ///     List of attachable volumes in use (mounted) by the node.
        /// </summary>
        [YamlMember(Alias = "volumesInUse")]
        [JsonProperty("volumesInUse", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> VolumesInUse { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="VolumesInUse"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVolumesInUse() => VolumesInUse.Count > 0;

        /// <summary>
        ///     Status of the config assigned to the node via the dynamic Kubelet config feature.
        /// </summary>
        [YamlMember(Alias = "config")]
        [JsonProperty("config", NullValueHandling = NullValueHandling.Ignore)]
        public NodeConfigStatusV1 Config { get; set; }

        /// <summary>
        ///     Set of ids/uuids to uniquely identify the node. More info: https://kubernetes.io/docs/concepts/nodes/node/#info
        /// </summary>
        [YamlMember(Alias = "nodeInfo")]
        [JsonProperty("nodeInfo", NullValueHandling = NullValueHandling.Ignore)]
        public NodeSystemInfoV1 NodeInfo { get; set; }

        /// <summary>
        ///     List of addresses reachable to the node. Queried from cloud provider, if available. More info: https://kubernetes.io/docs/concepts/nodes/node/#addresses
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "addresses")]
        [JsonProperty("addresses", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NodeAddressV1> Addresses { get; } = new List<NodeAddressV1>();

        /// <summary>
        ///     Determine whether the <see cref="Addresses"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAddresses() => Addresses.Count > 0;

        /// <summary>
        ///     Conditions is an array of current observed node conditions. More info: https://kubernetes.io/docs/concepts/nodes/node/#condition
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NodeConditionV1> Conditions { get; } = new List<NodeConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     Endpoints of daemons running on the Node.
        /// </summary>
        [YamlMember(Alias = "daemonEndpoints")]
        [JsonProperty("daemonEndpoints", NullValueHandling = NullValueHandling.Ignore)]
        public NodeDaemonEndpointsV1 DaemonEndpoints { get; set; }

        /// <summary>
        ///     List of container images on this node
        /// </summary>
        [YamlMember(Alias = "images")]
        [JsonProperty("images", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ContainerImageV1> Images { get; } = new List<ContainerImageV1>();

        /// <summary>
        ///     Determine whether the <see cref="Images"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeImages() => Images.Count > 0;

        /// <summary>
        ///     Capacity represents the total resources of a node. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#capacity
        /// </summary>
        [YamlMember(Alias = "capacity")]
        [JsonProperty("capacity", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Capacity { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Capacity"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCapacity() => Capacity.Count > 0;
    }
}
