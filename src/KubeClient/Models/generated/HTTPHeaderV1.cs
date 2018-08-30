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
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     The header field value
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public virtual string Value { get; set; }
    }
}
