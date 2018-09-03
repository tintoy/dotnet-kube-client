using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachmentStatus is the status of a VolumeAttachment request.
    /// </summary>
    public partial class VolumeAttachmentStatusV1Alpha1
    {
        /// <summary>
        ///     The last error encountered during detach operation, if any. This field must only be set by the entity completing the detach operation, i.e. the external-attacher.
        /// </summary>
        [JsonProperty("detachError")]
        [YamlMember(Alias = "detachError")]
        public VolumeErrorV1Alpha1 DetachError { get; set; }

        /// <summary>
        ///     Indicates the volume is successfully attached. This field must only be set by the entity completing the attach operation, i.e. the external-attacher.
        /// </summary>
        [JsonProperty("attached")]
        [YamlMember(Alias = "attached")]
        public bool Attached { get; set; }

        /// <summary>
        ///     Upon successful attach, this field is populated with any information returned by the attach operation that must be passed into subsequent WaitForAttach or Mount calls. This field must only be set by the entity completing the attach operation, i.e. the external-attacher.
        /// </summary>
        [YamlMember(Alias = "attachmentMetadata")]
        [JsonProperty("attachmentMetadata", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> AttachmentMetadata { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     The last error encountered during attach operation, if any. This field must only be set by the entity completing the attach operation, i.e. the external-attacher.
        /// </summary>
        [JsonProperty("attachError")]
        [YamlMember(Alias = "attachError")]
        public VolumeErrorV1Alpha1 AttachError { get; set; }
    }
}
