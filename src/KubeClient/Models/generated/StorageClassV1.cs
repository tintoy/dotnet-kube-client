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
    [KubeObject("StorageClass", "v1")]
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
        ///     Provisioner indicates the type of the provisioner.
        /// </summary>
        [JsonProperty("provisioner")]
        [YamlMember(Alias = "provisioner")]
        public string Provisioner { get; set; }

        /// <summary>
        ///     Parameters holds the parameters for the provisioner that should create volumes of this storage class.
        /// </summary>
        [YamlMember(Alias = "parameters")]
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
