using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceSliceSpec contains the information published by the driver in one ResourceSlice.
    /// </summary>
    public partial class ResourceSliceSpecV1Alpha3
    {
        /// <summary>
        ///     NodeName identifies the node which provides the resources in this pool. A field selector can be used to list only ResourceSlice objects belonging to a certain node.
        ///     
        ///     This field can be used to limit access from nodes to ResourceSlices with the same node name. It also indicates to autoscalers that adding new nodes of the same type as some old node might also make new resources available.
        ///     
        ///     Exactly one of NodeName, NodeSelector and AllNodes must be set. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "nodeName")]
        [JsonProperty("nodeName", NullValueHandling = NullValueHandling.Ignore)]
        public string NodeName { get; set; }

        /// <summary>
        ///     Pool describes the pool that this ResourceSlice belongs to.
        /// </summary>
        [YamlMember(Alias = "pool")]
        [JsonProperty("pool", NullValueHandling = NullValueHandling.Include)]
        public ResourcePoolV1Alpha3 Pool { get; set; }

        /// <summary>
        ///     Driver identifies the DRA driver providing the capacity information. A field selector can be used to list only ResourceSlice objects with a certain driver name.
        ///     
        ///     Must be a DNS subdomain and should end with a DNS domain owned by the vendor of the driver. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     NodeSelector defines which nodes have access to the resources in the pool, when that pool is not limited to a single node.
        ///     
        ///     Must use exactly one term.
        ///     
        ///     Exactly one of NodeName, NodeSelector and AllNodes must be set.
        /// </summary>
        [YamlMember(Alias = "nodeSelector")]
        [JsonProperty("nodeSelector", NullValueHandling = NullValueHandling.Ignore)]
        public NodeSelectorV1 NodeSelector { get; set; }

        /// <summary>
        ///     AllNodes indicates that all nodes have access to the resources in the pool.
        ///     
        ///     Exactly one of NodeName, NodeSelector and AllNodes must be set.
        /// </summary>
        [YamlMember(Alias = "allNodes")]
        [JsonProperty("allNodes", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllNodes { get; set; }

        /// <summary>
        ///     Devices lists some or all of the devices in this pool.
        ///     
        ///     Must not have more than 128 entries.
        /// </summary>
        [YamlMember(Alias = "devices")]
        [JsonProperty("devices", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceV1Alpha3> Devices { get; } = new List<DeviceV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Devices"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDevices() => Devices.Count > 0;
    }
}
