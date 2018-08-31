using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ControllerRevisionList is a resource containing a list of ControllerRevision objects.
    /// </summary>
    [KubeListItem("ControllerRevision", "apps/v1beta1")]
    [KubeObject("ControllerRevisionList", "apps/v1beta1")]
    public partial class ControllerRevisionListV1Beta1 : Models.ControllerRevisionListV1Beta1, ITracked
    {
        /// <summary>
        ///     Items is the list of ControllerRevisions
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ControllerRevisionV1Beta1> Items { get; } = new List<Models.ControllerRevisionV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
