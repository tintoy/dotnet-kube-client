using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IDRange provides a min/max of an allowed range of IDs. Deprecated: use IDRange from policy API Group instead.
    /// </summary>
    public partial class IDRangeV1Beta1
    {
        /// <summary>
        ///     min is the start of the range, inclusive.
        /// </summary>
        [YamlMember(Alias = "min")]
        [JsonProperty("min", NullValueHandling = NullValueHandling.Include)]
        public long Min { get; set; }

        /// <summary>
        ///     max is the end of the range, inclusive.
        /// </summary>
        [YamlMember(Alias = "max")]
        [JsonProperty("max", NullValueHandling = NullValueHandling.Include)]
        public long Max { get; set; }
    }
}
