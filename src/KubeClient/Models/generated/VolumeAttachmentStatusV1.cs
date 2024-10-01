using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachmentStatus is the status of a VolumeAttachment request.
    /// </summary>
    public partial class VolumeAttachmentStatusV1
    {
        /// <summary>
        ///     attachmentMetadata is populated with any information returned by the attach operation, upon successful attach, that must be passed into subsequent WaitForAttach or Mount calls. This field must only be set by the entity completing the attach operation, i.e. the external-attacher.
        /// </summary>
        [YamlMember(Alias = "attachmentMetadata")]
        [JsonProperty("attachmentMetadata", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> AttachmentMetadata { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="AttachmentMetadata"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAttachmentMetadata() => AttachmentMetadata.Count > 0;

        /// <summary>
        ///     attached indicates the volume is successfully attached. This field must only be set by the entity completing the attach operation, i.e. the external-attacher.
        /// </summary>
        [YamlMember(Alias = "attached")]
        [JsonProperty("attached", NullValueHandling = NullValueHandling.Include)]
        public bool Attached { get; set; }

        /// <summary>
        ///     attachError represents the last error encountered during attach operation, if any. This field must only be set by the entity completing the attach operation, i.e. the external-attacher.
        /// </summary>
        [YamlMember(Alias = "attachError")]
        [JsonProperty("attachError", NullValueHandling = NullValueHandling.Ignore)]
        public VolumeErrorV1 AttachError { get; set; }

        /// <summary>
        ///     detachError represents the last error encountered during detach operation, if any. This field must only be set by the entity completing the detach operation, i.e. the external-attacher.
        /// </summary>
        [YamlMember(Alias = "detachError")]
        [JsonProperty("detachError", NullValueHandling = NullValueHandling.Ignore)]
        public VolumeErrorV1 DetachError { get; set; }
    }
}
