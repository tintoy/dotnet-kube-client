using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaimList is a list of PersistentVolumeClaim items.
    /// </summary>
    [KubeObject("PersistentVolumeClaimList", "v1")]
    public class PersistentVolumeClaimListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     A list of persistent volume claims. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<PersistentVolumeClaimV1> Items { get; set; } = new List<PersistentVolumeClaimV1>();
    }
}
