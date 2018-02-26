using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodList is a list of Pods.
    /// </summary>
    [KubeObject("PodList", "v1")]
    public class PodListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of pods. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<PodV1> Items { get; set; } = new List<PodV1>();
    }
}
