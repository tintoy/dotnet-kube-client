using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StorageClass describes the parameters for a class of storage for which PersistentVolumes can be dynamically provisioned.
    ///     
    ///     StorageClasses are non-namespaced; the name of the storage class according to etcd is in ObjectMeta.Name.
    /// </summary>
    [KubeObject("StorageClass", "storage.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/storage.k8s.io/v1/storageclasses")]
    [KubeApi(KubeAction.Create, "apis/storage.k8s.io/v1/storageclasses")]
    [KubeApi(KubeAction.Get, "apis/storage.k8s.io/v1/storageclasses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storage.k8s.io/v1/storageclasses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storage.k8s.io/v1/storageclasses/{name}")]
    [KubeApi(KubeAction.Update, "apis/storage.k8s.io/v1/storageclasses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storage.k8s.io/v1/watch/storageclasses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storage.k8s.io/v1/storageclasses")]
    [KubeApi(KubeAction.Watch, "apis/storage.k8s.io/v1/watch/storageclasses/{name}")]
    public partial class StorageClassV1 : KubeResourceV1
    {
        /// <summary>
        ///     volumeBindingMode indicates how PersistentVolumeClaims should be provisioned and bound.  When unset, VolumeBindingImmediate is used. This field is only honored by servers that enable the VolumeScheduling feature.
        /// </summary>
        [YamlMember(Alias = "volumeBindingMode")]
        [JsonProperty("volumeBindingMode", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeBindingMode { get; set; }

        /// <summary>
        ///     allowVolumeExpansion shows whether the storage class allow volume expand.
        /// </summary>
        [YamlMember(Alias = "allowVolumeExpansion")]
        [JsonProperty("allowVolumeExpansion", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowVolumeExpansion { get; set; }

        /// <summary>
        ///     provisioner indicates the type of the provisioner.
        /// </summary>
        [YamlMember(Alias = "provisioner")]
        [JsonProperty("provisioner", NullValueHandling = NullValueHandling.Include)]
        public string Provisioner { get; set; }

        /// <summary>
        ///     allowedTopologies restrict the node topologies where volumes can be dynamically provisioned. Each volume plugin defines its own supported topology specifications. An empty TopologySelectorTerm list means there is no topology restriction. This field is only honored by servers that enable the VolumeScheduling feature.
        /// </summary>
        [YamlMember(Alias = "allowedTopologies")]
        [JsonProperty("allowedTopologies", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<TopologySelectorTermV1> AllowedTopologies { get; } = new List<TopologySelectorTermV1>();

        /// <summary>
        ///     Determine whether the <see cref="AllowedTopologies"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllowedTopologies() => AllowedTopologies.Count > 0;

        /// <summary>
        ///     mountOptions controls the mountOptions for dynamically provisioned PersistentVolumes of this storage class. e.g. ["ro", "soft"]. Not validated - mount of the PVs will simply fail if one is invalid.
        /// </summary>
        [YamlMember(Alias = "mountOptions")]
        [JsonProperty("mountOptions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> MountOptions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="MountOptions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMountOptions() => MountOptions.Count > 0;

        /// <summary>
        ///     parameters holds the parameters for the provisioner that should create volumes of this storage class.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        [JsonProperty("parameters", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Parameters"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeParameters() => Parameters.Count > 0;

        /// <summary>
        ///     reclaimPolicy controls the reclaimPolicy for dynamically provisioned PersistentVolumes of this storage class. Defaults to Delete.
        /// </summary>
        [YamlMember(Alias = "reclaimPolicy")]
        [JsonProperty("reclaimPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string ReclaimPolicy { get; set; }
    }
}
