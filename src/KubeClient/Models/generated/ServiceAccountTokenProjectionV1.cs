using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceAccountTokenProjection represents a projected service account token volume. This projection can be used to insert a service account token into the pods runtime filesystem for use against APIs (Kubernetes API Server or otherwise).
    /// </summary>
    public partial class ServiceAccountTokenProjectionV1
    {
        /// <summary>
        ///     Audience is the intended audience of the token. A recipient of a token must identify itself with an identifier specified in the audience of the token, and otherwise should reject the token. The audience defaults to the identifier of the apiserver.
        /// </summary>
        [YamlMember(Alias = "audience")]
        [JsonProperty("audience", NullValueHandling = NullValueHandling.Ignore)]
        public string Audience { get; set; }

        /// <summary>
        ///     Path is the path relative to the mount point of the file to project the token into.
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }

        /// <summary>
        ///     ExpirationSeconds is the requested duration of validity of the service account token. As the token approaches expiration, the kubelet volume plugin will proactively rotate the service account token. The kubelet will start trying to rotate the token if the token is older than 80 percent of its time to live or if the token is older than 24 hours.Defaults to 1 hour and must be at least 10 minutes.
        /// </summary>
        [YamlMember(Alias = "expirationSeconds")]
        [JsonProperty("expirationSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpirationSeconds { get; set; }
    }
}
