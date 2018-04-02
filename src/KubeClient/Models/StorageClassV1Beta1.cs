using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StorageClass describes the parameters for a class of storage for which PersistentVolumes can be dynamically provisioned.
    ///     
    ///     StorageClasses are non-namespaced; the name of the storage class according to etcd is in ObjectMeta.Name.
    /// </summary>
    [KubeObject("StorageClass", "storage.k8s.io/v1beta1")]
    public partial class StorageClassV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     AllowVolumeExpansion shows whether the storage class allow volume expand
        /// </summary>
        [JsonProperty("allowVolumeExpansion")]
        public bool AllowVolumeExpansion { get; set; }

        /// <summary>
        ///     Provisioner indicates the type of the provisioner.
        /// </summary>
        [JsonProperty("provisioner")]
        public string Provisioner { get; set; }

        /// <summary>
        ///     Dynamically provisioned PersistentVolumes of this storage class are created with these mountOptions, e.g. ["ro", "soft"]. Not validated - mount of the PVs will simply fail if one is invalid.
        /// </summary>
        [JsonProperty("mountOptions", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MountOptions { get; set; } = new List<string>();

        /// <summary>
        ///     Parameters holds the parameters for the provisioner that should create volumes of this storage class.
        /// </summary>
        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Dynamically provisioned PersistentVolumes of this storage class are created with this reclaimPolicy. Defaults to Delete.
        /// </summary>
        [JsonProperty("reclaimPolicy")]
        public string ReclaimPolicy { get; set; }
    }
}
