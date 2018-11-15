using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventSeries contain information on series of events, i.e. thing that was/is happening continuously for some time.
    /// </summary>
    public partial class EventSeriesV1
    {
        /// <summary>
        ///     Time of the last occurrence observed
        /// </summary>
        [YamlMember(Alias = "lastObservedTime")]
        [JsonProperty("lastObservedTime", NullValueHandling = NullValueHandling.Ignore)]
        public MicroTimeV1 LastObservedTime { get; set; }

        /// <summary>
        ///     State of this Series: Ongoing or Finished
        /// </summary>
        [YamlMember(Alias = "state")]
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        /// <summary>
        ///     Number of occurrences in this series up to the last heartbeat time
        /// </summary>
        [YamlMember(Alias = "count")]
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }
    }
}
