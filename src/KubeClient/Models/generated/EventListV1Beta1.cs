using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventList is a list of Event objects.
    /// </summary>
    [KubeListItem("Event", "events.k8s.io/v1beta1")]
    [KubeObject("EventList", "events.k8s.io/v1beta1")]
    public partial class EventListV1Beta1 : KubeResourceListV1<EventV1Beta1>
    {
        /// <summary>
        ///     Items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<EventV1Beta1> Items { get; } = new List<EventV1Beta1>();
    }
}
