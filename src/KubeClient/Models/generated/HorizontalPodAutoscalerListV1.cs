using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     list of horizontal pod autoscaler objects.
    /// </summary>
    public partial class HorizontalPodAutoscalerListV1 : KubeResourceListV1<HorizontalPodAutoscalerV1>
    {
        /// <summary>
        ///     list of horizontal pod autoscaler objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<HorizontalPodAutoscalerV1> Items { get; } = new List<HorizontalPodAutoscalerV1>();
    }
}
