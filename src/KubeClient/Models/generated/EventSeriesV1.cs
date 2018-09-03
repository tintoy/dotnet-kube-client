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
        ///     State of this Series: Ongoing or Finished
        /// </summary>
        [JsonProperty("state")]
        [YamlMember(Alias = "state")]
        public string State { get; set; }
    }
}
