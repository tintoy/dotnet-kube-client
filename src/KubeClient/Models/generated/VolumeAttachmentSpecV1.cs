using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachmentSpec is the specification of a VolumeAttachment request.
    /// </summary>
    public partial class VolumeAttachmentSpecV1
    {
        /// <summary>
        ///     nodeName represents the node that the volume should be attached to.
        /// </summary>
        [YamlMember(Alias = "nodeName")]
        [JsonProperty("nodeName", NullValueHandling = NullValueHandling.Include)]
        public string NodeName { get; set; }

        /// <summary>
        ///     source represents the volume that should be attached.
        /// </summary>
        [YamlMember(Alias = "source")]
        [JsonProperty("source", NullValueHandling = NullValueHandling.Include)]
        public VolumeAttachmentSourceV1 Source { get; set; }

        /// <summary>
        ///     attacher indicates the name of the volume driver that MUST handle this request. This is the name returned by GetPluginName().
        /// </summary>
        [YamlMember(Alias = "attacher")]
        [JsonProperty("attacher", NullValueHandling = NullValueHandling.Include)]
        public string Attacher { get; set; }
    }
}
