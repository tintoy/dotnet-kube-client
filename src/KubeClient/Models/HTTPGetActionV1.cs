using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     HTTPGetAction describes an action based on HTTP Get requests.
    /// </summary>
    [KubeObject("HTTPGetAction", "v1")]
    public partial class HTTPGetActionV1
    {
        /// <summary>
        ///     Scheme to use for connecting to the host. Defaults to HTTP.
        /// </summary>
        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        /// <summary>
        ///     Path to access on the HTTP server.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        ///     Custom headers to set in the request. HTTP allows repeated headers.
        /// </summary>
        [JsonProperty("httpHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public List<HTTPHeaderV1> HttpHeaders { get; set; } = new List<HTTPHeaderV1>();

        /// <summary>
        ///     Host name to connect to, defaults to the pod IP. You probably want to set "Host" in httpHeaders instead.
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; }

        /// <summary>
        ///     Name or number of the port to access on the container. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME.
        /// </summary>
        [JsonProperty("port")]
        public string Port { get; set; }
    }
}
