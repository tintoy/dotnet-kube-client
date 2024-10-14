using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceAllocationResult is the result of allocating devices.
    /// </summary>
    public partial class DeviceAllocationResultV1Alpha3
    {
        /// <summary>
        ///     This field is a combination of all the claim and class configuration parameters. Drivers can distinguish between those based on a flag.
        ///     
        ///     This includes configuration parameters for drivers which have no allocated devices in the result because it is up to the drivers which configuration parameters they support. They can silently ignore unknown configuration parameters.
        /// </summary>
        [YamlMember(Alias = "config")]
        [JsonProperty("config", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceAllocationConfigurationV1Alpha3> Config { get; } = new List<DeviceAllocationConfigurationV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Config"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConfig() => Config.Count > 0;

        /// <summary>
        ///     Results lists all allocated devices.
        /// </summary>
        [YamlMember(Alias = "results")]
        [JsonProperty("results", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceRequestAllocationResultV1Alpha3> Results { get; } = new List<DeviceRequestAllocationResultV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Results"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResults() => Results.Count > 0;
    }
}
