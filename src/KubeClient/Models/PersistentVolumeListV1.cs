using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeList is a list of PersistentVolume items.
    /// </summary>
    [KubeObject("PersistentVolumeList", "v1")]
    public class PersistentVolumeListV1 : KubeResourceListV1<PersistentVolumeV1>
    {
        /// <summary>
        ///     List of persistent volumes. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PersistentVolumeV1> Items { get; } = new List<PersistentVolumeV1>();
    }
}
