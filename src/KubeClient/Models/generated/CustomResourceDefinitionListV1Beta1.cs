using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionList is a list of CustomResourceDefinition objects.
    /// </summary>
    [KubeListItem("CustomResourceDefinition", "apiextensions.k8s.io/v1beta1")]
    [KubeObject("CustomResourceDefinitionList", "apiextensions.k8s.io/v1beta1")]
    public partial class CustomResourceDefinitionListV1Beta1 : KubeResourceListV1<CustomResourceDefinitionV1Beta1>
    {
        /// <summary>
        ///     Items individual CustomResourceDefinitions
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CustomResourceDefinitionV1Beta1> Items { get; } = new List<CustomResourceDefinitionV1Beta1>();
    }
}
