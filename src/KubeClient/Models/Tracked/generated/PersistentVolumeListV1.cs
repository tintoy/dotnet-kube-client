using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeList is a list of PersistentVolume items.
    /// </summary>
    [KubeListItem("PersistentVolume", "v1")]
    [KubeObject("PersistentVolumeList", "v1")]
    public partial class PersistentVolumeListV1 : Models.PersistentVolumeListV1, ITracked
    {
        /// <summary>
        ///     List of persistent volumes. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.PersistentVolumeV1> Items { get; } = new List<Models.PersistentVolumeV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
