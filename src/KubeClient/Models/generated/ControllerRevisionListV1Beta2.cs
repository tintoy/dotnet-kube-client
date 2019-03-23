using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ControllerRevisionList is a resource containing a list of ControllerRevision objects.
    /// </summary>
    [KubeListItem("ControllerRevision", "apps/v1beta2")]
    [KubeObject("ControllerRevisionList", "apps/v1beta2")]
    public partial class ControllerRevisionListV1Beta2 : KubeResourceListV1<ControllerRevisionV1Beta2>
    {
        /// <summary>
        ///     Items is the list of ControllerRevisions
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ControllerRevisionV1Beta2> Items { get; } = new List<ControllerRevisionV1Beta2>();
    }
}
