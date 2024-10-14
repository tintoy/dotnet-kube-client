using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowSchemaList is a list of FlowSchema objects.
    /// </summary>
    [KubeListItem("FlowSchema", "flowcontrol.apiserver.k8s.io/v1beta3")]
    [KubeObject("FlowSchemaList", "flowcontrol.apiserver.k8s.io/v1beta3")]
    public partial class FlowSchemaListV1Beta3 : KubeResourceListV1<FlowSchemaV1Beta3>
    {
        /// <summary>
        ///     `items` is a list of FlowSchemas.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<FlowSchemaV1Beta3> Items { get; } = new List<FlowSchemaV1Beta3>();
    }
}
