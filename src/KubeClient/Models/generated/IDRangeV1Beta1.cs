using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ID Range provides a min/max of an allowed range of IDs.
    /// </summary>
    public partial class IDRangeV1Beta1
    {
        /// <summary>
        ///     Min is the start of the range, inclusive.
        /// </summary>
        [JsonProperty("min")]
        [YamlMember(Alias = "min")]
        public virtual int Min { get; set; }

        /// <summary>
        ///     Max is the end of the range, inclusive.
        /// </summary>
        [JsonProperty("max")]
        [YamlMember(Alias = "max")]
        public virtual int Max { get; set; }
    }
}
