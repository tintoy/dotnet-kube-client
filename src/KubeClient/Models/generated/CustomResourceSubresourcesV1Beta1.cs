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
        ///     Status denotes the status subresource for CustomResources
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public CustomResourceSubresourceStatusV1Beta1 Status { get; set; }

        /// <summary>
        ///     Scale denotes the scale subresource for CustomResources
        /// </summary>
        [JsonProperty("scale")]
        [YamlMember(Alias = "scale")]
        public CustomResourceSubresourceScaleV1Beta1 Scale { get; set; }
    }
}
