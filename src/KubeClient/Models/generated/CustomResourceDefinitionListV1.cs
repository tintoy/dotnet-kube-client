using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionList is a list of CustomResourceDefinition objects.
    /// </summary>
    [KubeListItem("CustomResourceDefinition", "apiextensions.k8s.io/v1")]
    [KubeObject("CustomResourceDefinitionList", "apiextensions.k8s.io/v1")]
    public partial class CustomResourceDefinitionListV1 : KubeResourceListV1<CustomResourceDefinitionV1>
    {
        /// <summary>
        ///     items list individual CustomResourceDefinition objects
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CustomResourceDefinitionV1> Items { get; } = new List<CustomResourceDefinitionV1>();
    }
}
