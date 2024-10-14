using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceRequestAllocationResult contains the allocation result for one request.
    /// </summary>
    public partial class DeviceRequestAllocationResultV1Alpha3
    {
        /// <summary>
        ///     Device references one device instance via its name in the driver's resource pool. It must be a DNS label.
        /// </summary>
        [YamlMember(Alias = "device")]
        [JsonProperty("device", NullValueHandling = NullValueHandling.Include)]
        public string Device { get; set; }

        /// <summary>
        ///     This name together with the driver name and the device name field identify which device was allocated (`&lt;driver name&gt;/&lt;pool name&gt;/&lt;device name&gt;`).
        ///     
        ///     Must not be longer than 253 characters and may contain one or more DNS sub-domains separated by slashes.
        /// </summary>
        [YamlMember(Alias = "pool")]
        [JsonProperty("pool", NullValueHandling = NullValueHandling.Include)]
        public string Pool { get; set; }

        /// <summary>
        ///     Driver specifies the name of the DRA driver whose kubelet plugin should be invoked to process the allocation once the claim is needed on a node.
        ///     
        ///     Must be a DNS subdomain and should end with a DNS domain owned by the vendor of the driver.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     Request is the name of the request in the claim which caused this device to be allocated. Multiple devices may have been allocated per request.
        /// </summary>
        [YamlMember(Alias = "request")]
        [JsonProperty("request", NullValueHandling = NullValueHandling.Include)]
        public string Request { get; set; }
    }
}
