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
        [JsonProperty("min")]
        [YamlMember(Alias = "min")]
        public int Min { get; set; }

        /// <summary>
        ///     max is the end of the range, inclusive.
        /// </summary>
        [JsonProperty("max")]
        [YamlMember(Alias = "max")]
        public int Max { get; set; }
    }
}
