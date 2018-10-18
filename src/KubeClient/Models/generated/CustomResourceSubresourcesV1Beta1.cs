using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceSubresources defines the status and scale subresources for CustomResources.
    /// </summary>
    public partial class CustomResourceSubresourcesV1Beta1
    {
        /// <summary>
        ///     Scale denotes the scale subresource for CustomResources
        /// </summary>
        [YamlMember(Alias = "scale")]
        [JsonProperty("scale", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceSubresourceScaleV1Beta1 Scale { get; set; }

        /// <summary>
        ///     Status denotes the status subresource for CustomResources
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceSubresourceStatusV1Beta1 Status { get; set; }
    }
}
