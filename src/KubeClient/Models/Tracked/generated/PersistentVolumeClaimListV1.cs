using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeClaimList is a list of PersistentVolumeClaim items.
    /// </summary>
    [KubeListItem("PersistentVolumeClaim", "v1")]
    [KubeObject("PersistentVolumeClaimList", "v1")]
    public partial class PersistentVolumeClaimListV1 : Models.PersistentVolumeClaimListV1, ITracked
    {
        /// <summary>
        ///     A list of persistent volume claims. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.PersistentVolumeClaimV1> Items { get; } = new List<Models.PersistentVolumeClaimV1>();
    }
}
