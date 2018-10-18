using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HostPortRange defines a range of host ports that will be enabled by a policy for pods to use.  It requires both the start and end to be defined. Deprecated: use HostPortRange from policy API Group instead.
    /// </summary>
    public partial class HostPortRangeV1Beta1
    {
        /// <summary>
        ///     min is the start of the range, inclusive.
        /// </summary>
        [YamlMember(Alias = "min")]
        [JsonProperty("min", NullValueHandling = NullValueHandling.Include)]
        public int Min { get; set; }

        /// <summary>
        ///     max is the end of the range, inclusive.
        /// </summary>
        [YamlMember(Alias = "max")]
        [JsonProperty("max", NullValueHandling = NullValueHandling.Include)]
        public int Max { get; set; }
    }
}
