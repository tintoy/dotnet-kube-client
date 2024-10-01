using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceSubresources defines the status and scale subresources for CustomResources.
    /// </summary>
    public partial class CustomResourceSubresourcesV1
    {
        /// <summary>
        ///     scale indicates the custom resource should serve a `/scale` subresource that returns an `autoscaling/v1` Scale object.
        /// </summary>
        [YamlMember(Alias = "scale")]
        [JsonProperty("scale", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceSubresourceScaleV1 Scale { get; set; }

        /// <summary>
        ///     status indicates the custom resource should serve a `/status` subresource. When enabled: 1. requests to the custom resource primary endpoint ignore changes to the `status` stanza of the object. 2. requests to the custom resource `/status` subresource ignore changes to anything other than the `status` stanza of the object.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceSubresourceStatusV1 Status { get; set; }
    }
}
