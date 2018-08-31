using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodList is a list of Pods.
    /// </summary>
    [KubeListItem("Pod", "v1")]
    [KubeObject("PodList", "v1")]
    public partial class PodListV1 : Models.PodListV1, ITracked
    {
        /// <summary>
        ///     List of pods. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.PodV1> Items { get; } = new List<Models.PodV1>();
    }
}
