using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HTTPHeader describes a custom header to be used in HTTP probes
    /// </summary>
    public partial class HTTPHeaderV1
    {
        /// <summary>
        ///     The header field name
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     The header field value
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Include)]
        public string Value { get; set; }
    }
}
