using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeList is a list of PersistentVolume items.
    /// </summary>
    [KubeResource("PersistentVolumeList", "v1")]
    public class PersistentVolumeListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of persistent volumes. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<PersistentVolumeV1> Items { get; set; } = new List<PersistentVolumeV1>();
    }
}
