using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ControllerRevisionList is a resource containing a list of ControllerRevision objects.
    /// </summary>
    [KubeObject("ControllerRevisionList", "apps/v1beta1")]
    public class ControllerRevisionListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is the list of ControllerRevisions
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ControllerRevisionV1Beta1> Items { get; set; } = new List<ControllerRevisionV1Beta1>();
    }
}
