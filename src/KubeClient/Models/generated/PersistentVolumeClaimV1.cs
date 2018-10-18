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
    [KubeApi(KubeAction.List, "api/v1/persistentvolumeclaims")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/persistentvolumeclaims")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/persistentvolumeclaims")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/persistentvolumeclaims")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/persistentvolumeclaims")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/persistentvolumeclaims")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/persistentvolumeclaims/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}/status")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/persistentvolumeclaims/{name}/status")]
    public partial class PersistentVolumeClaimV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired characteristics of a volume requested by a pod author. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeClaimSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status represents the current information/status of a persistent volume claim. Read-only. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeClaimStatusV1 Status { get; set; }
    }
}
