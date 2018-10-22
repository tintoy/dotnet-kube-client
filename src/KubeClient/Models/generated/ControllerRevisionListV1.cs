using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ControllerRevisionList is a resource containing a list of ControllerRevision objects.
    /// </summary>
    [KubeListItem("ControllerRevision", "apps/v1")]
    [KubeObject("ControllerRevisionList", "apps/v1")]
    public partial class ControllerRevisionListV1 : KubeResourceListV1<ControllerRevisionV1>
    {
        /// <summary>
        ///     Items is the list of ControllerRevisions
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ControllerRevisionV1> Items { get; } = new List<ControllerRevisionV1>();
    }
}
