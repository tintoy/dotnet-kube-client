using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaim is a user's request for and claim to a persistent volume
    /// </summary>
    [KubeObject("PersistentVolumeClaim", "v1")]
    [KubeApi("api/v1/persistentvolumeclaims", KubeAction.List)]
    [KubeApi("api/v1/watch/persistentvolumeclaims", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/persistentvolumeclaims", KubeAction.WatchList)]
    [KubeApi("api/v1/watch/namespaces/{namespace}/persistentvolumeclaims/{name}", KubeAction.Watch)]
    [KubeApi("api/v1/namespaces/{namespace}/persistentvolumeclaims", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    public partial class PersistentVolumeClaimV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired characteristics of a volume requested by a pod author. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public PersistentVolumeClaimSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status represents the current information/status of a persistent volume claim. Read-only. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public PersistentVolumeClaimStatusV1 Status { get; set; }
    }
}
