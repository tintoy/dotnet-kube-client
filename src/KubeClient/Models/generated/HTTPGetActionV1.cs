using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HTTPGetAction describes an action based on HTTP Get requests.
    /// </summary>
    public partial class HTTPGetActionV1
    {
        /// <summary>
        ///     Scheme to use for connecting to the host. Defaults to HTTP.
        /// </summary>
        [YamlMember(Alias = "scheme")]
        [JsonProperty("scheme", NullValueHandling = NullValueHandling.Ignore)]
        public string Scheme { get; set; }

        /// <summary>
        ///     Path to access on the HTTP server.
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        /// <summary>
        ///     Custom headers to set in the request. HTTP allows repeated headers.
        /// </summary>
        [YamlMember(Alias = "httpHeaders")]
        [JsonProperty("httpHeaders", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<HTTPHeaderV1> HttpHeaders { get; } = new List<HTTPHeaderV1>();

        /// <summary>
        ///     Determine whether the <see cref="HttpHeaders"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHttpHeaders() => HttpHeaders.Count > 0;

        /// <summary>
        ///     Host name to connect to, defaults to the pod IP. You probably want to set "Host" in httpHeaders instead.
        /// </summary>
        [YamlMember(Alias = "host")]
        [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
        public string Host { get; set; }

        /// <summary>
        ///     Name or number of the port to access on the container. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME.
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Include)]
        public Int32OrStringV1 Port { get; set; }
    }
}
