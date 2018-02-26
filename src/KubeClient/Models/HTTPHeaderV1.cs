using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     HTTPHeader describes a custom header to be used in HTTP probes
    /// </summary>
    [KubeObject("HTTPHeader", "v1")]
    public class HTTPHeaderV1
    {
        /// <summary>
        ///     The header field name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     The header field value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
