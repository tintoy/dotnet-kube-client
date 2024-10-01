using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AllocationResult contains attributes of an allocated resource.
    /// </summary>
    public partial class AllocationResultV1Alpha3
    {
        /// <summary>
        ///     Controller is the name of the DRA driver which handled the allocation. That driver is also responsible for deallocating the claim. It is empty when the claim can be deallocated without involving a driver.
        ///     
        ///     A driver may allocate devices provided by other drivers, so this driver name here can be different from the driver names listed for the results.
        ///     
        ///     This is an alpha field and requires enabling the DRAControlPlaneController feature gate.
        /// </summary>
        [YamlMember(Alias = "controller")]
        [JsonProperty("controller", NullValueHandling = NullValueHandling.Ignore)]
        public string Controller { get; set; }

        /// <summary>
        ///     NodeSelector defines where the allocated resources are available. If unset, they are available everywhere.
        /// </summary>
        [YamlMember(Alias = "nodeSelector")]
        [JsonProperty("nodeSelector", NullValueHandling = NullValueHandling.Ignore)]
        public NodeSelectorV1 NodeSelector { get; set; }

        /// <summary>
        ///     Devices is the result of allocating devices.
        /// </summary>
        [YamlMember(Alias = "devices")]
        [JsonProperty("devices", NullValueHandling = NullValueHandling.Ignore)]
        public DeviceAllocationResultV1Alpha3 Devices { get; set; }
    }
}
