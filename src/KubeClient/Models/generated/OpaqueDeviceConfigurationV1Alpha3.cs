using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     OpaqueDeviceConfiguration contains configuration parameters for a driver in a format defined by the driver vendor.
    /// </summary>
    public partial class OpaqueDeviceConfigurationV1Alpha3
    {
        /// <summary>
        ///     Driver is used to determine which kubelet plugin needs to be passed these configuration parameters.
        ///     
        ///     An admission policy provided by the driver developer could use this to decide whether it needs to validate them.
        ///     
        ///     Must be a DNS subdomain and should end with a DNS domain owned by the vendor of the driver.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     Parameters can contain arbitrary data. It is the responsibility of the driver developer to handle validation and versioning. Typically this includes self-identification and a version ("kind" + "apiVersion" for Kubernetes types), with conversion between different versions.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Include)]
        public RawExtensionRuntime Parameters { get; set; }
    }
}
