using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CustomResourceDefinitionList is a list of CustomResourceDefinition objects.
    /// </summary>
    [KubeListItem("CustomResourceDefinition", "v1beta1")]
    [KubeObject("CustomResourceDefinitionList", "v1beta1")]
    public partial class CustomResourceDefinitionListV1Beta1 : Models.CustomResourceDefinitionListV1Beta1, ITracked
    {
        /// <summary>
        ///     Items individual CustomResourceDefinitions
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.CustomResourceDefinitionV1Beta1> Items { get; } = new List<Models.CustomResourceDefinitionV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
