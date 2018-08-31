using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodList is a list of Pods.
    /// </summary>
    [KubeListItem("Pod", "v1")]
    [KubeObject("PodList", "v1")]
    public partial class PodListV1 : KubeResourceListV1<PodV1>
    {
        /// <summary>
        ///     List of pods. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodV1> Items { get; } = new List<PodV1>();
    }
}
