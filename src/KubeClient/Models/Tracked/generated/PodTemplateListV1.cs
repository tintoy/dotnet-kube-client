using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodTemplateList is a list of PodTemplates.
    /// </summary>
    [KubeListItem("PodTemplate", "v1")]
    [KubeObject("PodTemplateList", "v1")]
    public partial class PodTemplateListV1 : Models.PodTemplateListV1, ITracked
    {
        /// <summary>
        ///     List of pod templates
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.PodTemplateV1> Items { get; } = new List<Models.PodTemplateV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
