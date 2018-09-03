using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventSeries contain information on series of events, i.e. thing that was/is happening continuously for some time.
    /// </summary>
    public partial class EventSeriesV1Beta1
    {
        /// <summary>
        ///     Time when last Event from the series was seen before last heartbeat.
        /// </summary>
        [JsonProperty("lastObservedTime")]
        [YamlMember(Alias = "lastObservedTime")]
        public MicroTimeV1 LastObservedTime { get; set; }

        /// <summary>
        ///     Number of occurrences in this series up to the last heartbeat time
        /// </summary>
        [JsonProperty("count")]
        [YamlMember(Alias = "count")]
        public int Count { get; set; }

        /// <summary>
        ///     Information whether this series is ongoing or finished.
        /// </summary>
        [JsonProperty("state")]
        [YamlMember(Alias = "state")]
        public string State { get; set; }
    }
}
