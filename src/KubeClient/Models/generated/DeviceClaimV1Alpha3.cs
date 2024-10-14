using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceClaim defines how to request devices with a ResourceClaim.
    /// </summary>
    public partial class DeviceClaimV1Alpha3
    {
        /// <summary>
        ///     This field holds configuration for multiple potential drivers which could satisfy requests in this claim. It is ignored while allocating the claim.
        /// </summary>
        [YamlMember(Alias = "config")]
        [JsonProperty("config", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceClaimConfigurationV1Alpha3> Config { get; } = new List<DeviceClaimConfigurationV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Config"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConfig() => Config.Count > 0;

        /// <summary>
        ///     These constraints must be satisfied by the set of devices that get allocated for the claim.
        /// </summary>
        [YamlMember(Alias = "constraints")]
        [JsonProperty("constraints", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceConstraintV1Alpha3> Constraints { get; } = new List<DeviceConstraintV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Constraints"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConstraints() => Constraints.Count > 0;

        /// <summary>
        ///     Requests represent individual requests for distinct devices which must all be satisfied. If empty, nothing needs to be allocated.
        /// </summary>
        [YamlMember(Alias = "requests")]
        [JsonProperty("requests", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceRequestV1Alpha3> Requests { get; } = new List<DeviceRequestV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Requests"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRequests() => Requests.Count > 0;
    }
}
