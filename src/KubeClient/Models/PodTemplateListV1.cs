using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodTemplateList is a list of PodTemplates.
    /// </summary>
    [KubeObject("PodTemplateList", "v1")]
    public class PodTemplateListV1 : KubeResourceListV1<PodTemplateV1>
    {
        /// <summary>
        ///     List of pod templates
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PodTemplateV1> Items { get; } = new List<PodTemplateV1>();
    }
}
