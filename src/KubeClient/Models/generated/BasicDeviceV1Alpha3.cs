using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     BasicDevice defines one device instance.
    /// </summary>
    public partial class BasicDeviceV1Alpha3
    {
        /// <summary>
        ///     Attributes defines the set of attributes for this device. The name of each attribute must be unique in that set.
        ///     
        ///     The maximum number of attributes and capacities combined is 32.
        /// </summary>
        [YamlMember(Alias = "attributes")]
        [JsonProperty("attributes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, DeviceAttributeV1Alpha3> Attributes { get; } = new Dictionary<string, DeviceAttributeV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="Attributes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAttributes() => Attributes.Count > 0;

        /// <summary>
        ///     Capacity defines the set of capacities for this device. The name of each capacity must be unique in that set.
        ///     
        ///     The maximum number of attributes and capacities combined is 32.
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
