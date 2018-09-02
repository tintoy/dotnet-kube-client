using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ControllerRevisionList is a resource containing a list of ControllerRevision objects.
    /// </summary>
    public partial class ControllerRevisionListV1Beta1 : KubeResourceListV1<ControllerRevisionV1Beta1>
    {
        /// <summary>
        ///     Items is the list of ControllerRevisions
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ControllerRevisionV1Beta1> Items { get; } = new List<ControllerRevisionV1Beta1>();
    }
}
