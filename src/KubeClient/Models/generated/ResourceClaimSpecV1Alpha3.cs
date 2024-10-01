using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimSpec defines what is being requested in a ResourceClaim and how to configure it.
    /// </summary>
    public partial class ResourceClaimSpecV1Alpha3
    {
        /// <summary>
        ///     Controller is the name of the DRA driver that is meant to handle allocation of this claim. If empty, allocation is handled by the scheduler while scheduling a pod.
        ///     
        ///     Must be a DNS subdomain and should end with a DNS domain owned by the vendor of the driver.
        ///     
        ///     This is an alpha field and requires enabling the DRAControlPlaneController feature gate.
        /// </summary>
        [YamlMember(Alias = "controller")]
        [JsonProperty("controller", NullValueHandling = NullValueHandling.Ignore)]
        public string Controller { get; set; }

        /// <summary>
        ///     Devices defines how to request devices.
        /// </summary>
        [YamlMember(Alias = "devices")]
        [JsonProperty("devices", NullValueHandling = NullValueHandling.Ignore)]
        public DeviceClaimV1Alpha3 Devices { get; set; }
    }
}
