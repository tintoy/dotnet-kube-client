using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Host Port Range defines a range of host ports that will be enabled by a policy for pods to use.  It requires both the start and end to be defined.
    /// </summary>
    public partial class HostPortRangeV1Beta1
    {
        /// <summary>
        ///     min is the start of the range, inclusive.
        /// </summary>
        [JsonProperty("min")]
        [YamlMember(Alias = "min")]
        public virtual int Min { get; set; }

        /// <summary>
        ///     max is the end of the range, inclusive.
        /// </summary>
        [JsonProperty("max")]
        [YamlMember(Alias = "max")]
        public virtual int Max { get; set; }
    }
}
