using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodTemplateList is a list of PodTemplates.
    /// </summary>
    [KubeResource("PodTemplateList", "v1")]
    public class PodTemplateListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of pod templates
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<PodTemplateV1> Items { get; set; } = new List<PodTemplateV1>();
    }
}
