using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolume (PV) is a storage resource provisioned by an administrator. It is analogous to a node. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes
    /// </summary>
    [KubeObject("PersistentVolume", "v1")]
    [KubeApi(KubeAction.List, "api/v1/persistentvolumes")]
    [KubeApi(KubeAction.Create, "api/v1/persistentvolumes")]
    [KubeApi(KubeAction.Get, "api/v1/persistentvolumes/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/persistentvolumes/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/persistentvolumes/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/persistentvolumes/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/persistentvolumes")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/persistentvolumes")]
    [KubeApi(KubeAction.Get, "api/v1/persistentvolumes/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/persistentvolumes/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/persistentvolumes/{name}/status")]
    [KubeApi(KubeAction.Update, "api/v1/persistentvolumes/{name}/status")]
    public partial class PersistentVolumeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines a specification of a persistent volume owned by the cluster. Provisioned by an administrator. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistent-volumes
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status represents the current information/status for the persistent volume. Populated by the system. Read-only. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistent-volumes
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public PersistentVolumeStatusV1 Status { get; set; }
    }
}
