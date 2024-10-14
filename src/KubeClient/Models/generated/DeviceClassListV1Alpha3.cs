using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceClassList is a collection of classes.
    /// </summary>
    [KubeListItem("DeviceClass", "resource.k8s.io/v1alpha3")]
    [KubeObject("DeviceClassList", "resource.k8s.io/v1alpha3")]
    public partial class DeviceClassListV1Alpha3 : KubeResourceListV1<DeviceClassV1Alpha3>
    {
        /// <summary>
        ///     Items is the list of resource classes.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<DeviceClassV1Alpha3> Items { get; } = new List<DeviceClassV1Alpha3>();
    }
}
