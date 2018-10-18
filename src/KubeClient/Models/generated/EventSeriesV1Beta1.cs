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
        [YamlMember(Alias = "lastObservedTime")]
        [JsonProperty("lastObservedTime", NullValueHandling = NullValueHandling.Include)]
        public MicroTimeV1 LastObservedTime { get; set; }

        /// <summary>
        ///     Information whether this series is ongoing or finished.
        /// </summary>
        [YamlMember(Alias = "state")]
        [JsonProperty("state", NullValueHandling = NullValueHandling.Include)]
        public string State { get; set; }

        /// <summary>
        ///     Number of occurrences in this series up to the last heartbeat time
        /// </summary>
        [YamlMember(Alias = "count")]
        [JsonProperty("count", NullValueHandling = NullValueHandling.Include)]
        public int Count { get; set; }
    }
}
