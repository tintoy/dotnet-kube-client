using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowSchemaList is a list of FlowSchema objects.
    /// </summary>
    [KubeListItem("FlowSchema", "flowcontrol.apiserver.k8s.io/v1")]
    [KubeObject("FlowSchemaList", "flowcontrol.apiserver.k8s.io/v1")]
    public partial class FlowSchemaListV1 : KubeResourceListV1<FlowSchemaV1>
    {
        /// <summary>
        ///     `items` is a list of FlowSchemas.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<FlowSchemaV1> Items { get; } = new List<FlowSchemaV1>();
    }
}
