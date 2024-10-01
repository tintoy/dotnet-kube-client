using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventSeries contain information on series of events, i.e. thing that was/is happening continuously for some time. How often to update the EventSeries is up to the event reporters. The default event reporter in "k8s.io/client-go/tools/events/event_broadcaster.go" shows how this struct is updated on heartbeats and can guide customized reporter implementations.
    /// </summary>
    public partial class EventSeriesV1
    {
        /// <summary>
        ///     lastObservedTime is the time when last Event from the series was seen before last heartbeat.
        /// </summary>
        [YamlMember(Alias = "lastObservedTime")]
        [JsonProperty("lastObservedTime", NullValueHandling = NullValueHandling.Include)]
        public DateTime? LastObservedTime { get; set; }

        /// <summary>
        ///     count is the number of occurrences in this series up to the last heartbeat time.
        /// </summary>
        [YamlMember(Alias = "count")]
        [JsonProperty("count", NullValueHandling = NullValueHandling.Include)]
        public int Count { get; set; }
    }
}
