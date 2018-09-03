using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachmentList is a collection of VolumeAttachment objects.
    /// </summary>
    [KubeListItem("VolumeAttachment", "v1alpha1")]
    [KubeObject("VolumeAttachmentList", "v1alpha1")]
    public partial class VolumeAttachmentListV1Alpha1 : KubeResourceListV1<VolumeAttachmentV1Alpha1>
    {
        /// <summary>
        ///     Items is the list of VolumeAttachments
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<VolumeAttachmentV1Alpha1> Items { get; } = new List<VolumeAttachmentV1Alpha1>();
    }
}
