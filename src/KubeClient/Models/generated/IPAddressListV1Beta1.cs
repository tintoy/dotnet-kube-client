using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IPAddressList contains a list of IPAddress.
    /// </summary>
    [KubeListItem("IPAddress", "networking.k8s.io/v1beta1")]
    [KubeObject("IPAddressList", "networking.k8s.io/v1beta1")]
    public partial class IPAddressListV1Beta1 : KubeResourceListV1<IPAddressV1Beta1>
    {
        /// <summary>
        ///     items is the list of IPAddresses.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<IPAddressV1Beta1> Items { get; } = new List<IPAddressV1Beta1>();
    }
}
