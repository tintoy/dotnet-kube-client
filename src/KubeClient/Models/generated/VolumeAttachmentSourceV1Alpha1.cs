using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachmentSource represents a volume that should be attached. Right now only PersistenVolumes can be attached via external attacher, in future we may allow also inline volumes in pods. Exactly one member can be set.
    /// </summary>
    public partial class VolumeAttachmentSourceV1Alpha1
    {
        /// <summary>
        ///     Name of the persistent volume to attach.
        /// </summary>
        [YamlMember(Alias = "persistentVolumeName")]
        [JsonProperty("persistentVolumeName", NullValueHandling = NullValueHandling.Ignore)]
        public string PersistentVolumeName { get; set; }
    }
}
