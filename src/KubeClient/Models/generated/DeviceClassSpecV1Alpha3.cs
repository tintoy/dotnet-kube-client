using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceClassSpec is used in a [DeviceClass] to define what can be allocated and how to configure it.
    /// </summary>
    public partial class DeviceClassSpecV1Alpha3
    {
        /// <summary>
        ///     Config defines configuration parameters that apply to each device that is claimed via this class. Some classses may potentially be satisfied by multiple drivers, so each instance of a vendor configuration applies to exactly one driver.
        ///     
        ///     They are passed to the driver, but are not considered while allocating the claim.
        /// </summary>
        [YamlMember(Alias = "config")]
        [JsonProperty("config", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceClassConfigurationV1Alpha3> Config { get; } = new List<DeviceClassConfigurationV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Config"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConfig() => Config.Count > 0;

        /// <summary>
        ///     Each selector must be satisfied by a device which is claimed via this class.
        /// </summary>
        [YamlMember(Alias = "selectors")]
        [JsonProperty("selectors", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeviceSelectorV1Alpha3> Selectors { get; } = new List<DeviceSelectorV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Selectors"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSelectors() => Selectors.Count > 0;

        /// <summary>
        ///     Only nodes matching the selector will be considered by the scheduler when trying to find a Node that fits a Pod when that Pod uses a claim that has not been allocated yet *and* that claim gets allocated through a control plane controller. It is ignored when the claim does not use a control plane controller for allocation.
        ///     
        ///     Setting this field is optional. If unset, all Nodes are candidates.
        ///     
        ///     This is an alpha field and requires enabling the DRAControlPlaneController feature gate.
        /// </summary>
        [YamlMember(Alias = "suitableNodes")]
        [JsonProperty("suitableNodes", NullValueHandling = NullValueHandling.Ignore)]
        public NodeSelectorV1 SuitableNodes { get; set; }
    }
}
