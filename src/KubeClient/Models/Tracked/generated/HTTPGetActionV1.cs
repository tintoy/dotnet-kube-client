using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     HTTPGetAction describes an action based on HTTP Get requests.
    /// </summary>
    public partial class HTTPGetActionV1 : Models.HTTPGetActionV1, ITracked
    {
        /// <summary>
        ///     Scheme to use for connecting to the host. Defaults to HTTP.
        /// </summary>
        [JsonProperty("scheme")]
        [YamlMember(Alias = "scheme")]
        public override string Scheme
        {
            get
            {
                return base.Scheme;
            }
            set
            {
                base.Scheme = value;

                __ModifiedProperties__.Add("Scheme");
            }
        }


        /// <summary>
        ///     Path to access on the HTTP server.
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public override string Path
        {
            get
            {
                return base.Path;
            }
            set
            {
                base.Path = value;

                __ModifiedProperties__.Add("Path");
            }
        }


        /// <summary>
        ///     Custom headers to set in the request. HTTP allows repeated headers.
        /// </summary>
        [YamlMember(Alias = "httpHeaders")]
        [JsonProperty("httpHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.HTTPHeaderV1> HttpHeaders { get; set; } = new List<Models.HTTPHeaderV1>();

        /// <summary>
        ///     Host name to connect to, defaults to the pod IP. You probably want to set "Host" in httpHeaders instead.
        /// </summary>
        [JsonProperty("host")]
        [YamlMember(Alias = "host")]
        public override string Host
        {
            get
            {
                return base.Host;
            }
            set
            {
                base.Host = value;

                __ModifiedProperties__.Add("Host");
            }
        }


        /// <summary>
        ///     Name or number of the port to access on the container. Number must be in the range 1 to 65535. Name must be an IANA_SVC_NAME.
        /// </summary>
        [JsonProperty("port")]
        [YamlMember(Alias = "port")]
        public override string Port
        {
            get
            {
                return base.Port;
            }
            set
            {
                base.Port = value;

                __ModifiedProperties__.Add("Port");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
