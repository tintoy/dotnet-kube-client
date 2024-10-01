using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSchedulingContextList is a collection of Pod scheduling objects.
    /// </summary>
    [KubeListItem("PodSchedulingContext", "resource.k8s.io/v1alpha3")]
    [KubeObject("PodSchedulingContextList", "resource.k8s.io/v1alpha3")]
    public partial class PodSchedulingContextListV1Alpha3 : KubeResourceListV1<PodSchedulingContextV1Alpha3>
    {
        /// <summary>
        ///     Items is the list of PodSchedulingContext objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodSchedulingContextV1Alpha3> Items { get; } = new List<PodSchedulingContextV1Alpha3>();
    }
}
