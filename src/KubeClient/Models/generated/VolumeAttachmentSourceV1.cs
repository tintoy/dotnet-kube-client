using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachmentSource represents a volume that should be attached. Right now only PersistenVolumes can be attached via external attacher, in future we may allow also inline volumes in pods. Exactly one member can be set.
    /// </summary>
    public partial class VolumeAttachmentSourceV1
    {
        /// <summary>
        ///     inlineVolumeSpec contains all the information necessary to attach a persistent volume defined by a pod's inline VolumeSource. This field is populated only for the CSIMigration feature. It contains translated fields from a pod's inline VolumeSource to a PersistentVolumeSpec. This field is beta-level and is only honored by servers that enabled the CSIMigration feature.
        /// </summary>
        [YamlMember(Alias = "inlineVolumeSpec")]
        [JsonProperty("inlineVolumeSpec", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeSpecV1 InlineVolumeSpec { get; set; }

        /// <summary>
        ///     persistentVolumeName represents the name of the persistent volume to attach.
        /// </summary>
        [YamlMember(Alias = "persistentVolumeName")]
        [JsonProperty("persistentVolumeName", NullValueHandling = NullValueHandling.Ignore)]
        public string PersistentVolumeName { get; set; }
    }
}
