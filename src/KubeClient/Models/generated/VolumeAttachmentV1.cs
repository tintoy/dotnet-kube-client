using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeAttachment captures the intent to attach or detach the specified volume to/from the specified node.
    ///     
    ///     VolumeAttachment objects are non-namespaced.
    /// </summary>
    [KubeObject("VolumeAttachment", "storage.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1/volumeattachments")]
    [KubeApi(KubeAction.Create, "apis/storage.k8s.io/v1/volumeattachments")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1/volumeattachments/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1/volumeattachments/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storage.k8s.io/v1/volumeattachments/{name}")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1/volumeattachments/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1/watch/volumeattachments")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storage.k8s.io/v1/volumeattachments")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1/volumeattachments/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/storage.k8s.io/v1/watch/volumeattachments/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1/volumeattachments/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1/volumeattachments/{name}/status")]
    public partial class VolumeAttachmentV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec represents specification of the desired attach/detach volume behavior. Populated by the Kubernetes system.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public VolumeAttachmentSpecV1 Spec { get; set; }

        /// <summary>
        ///     status represents status of the VolumeAttachment request. Populated by the entity completing the attach or detach operation, i.e. the external-attacher.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public VolumeAttachmentStatusV1 Status { get; set; }
    }
}
