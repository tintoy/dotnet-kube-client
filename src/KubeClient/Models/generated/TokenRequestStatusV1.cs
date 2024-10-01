using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenRequestStatus is the result of a token request.
    /// </summary>
    public partial class TokenRequestStatusV1
    {
        /// <summary>
        ///     Token is the opaque bearer token.
        /// </summary>
        [YamlMember(Alias = "token")]
        [JsonProperty("token", NullValueHandling = NullValueHandling.Include)]
        public string Token { get; set; }

        /// <summary>
        ///     ExpirationTimestamp is the time of expiration of the returned token.
        /// </summary>
        [YamlMember(Alias = "expirationTimestamp")]
        [JsonProperty("expirationTimestamp", NullValueHandling = NullValueHandling.Include)]
        public DateTime? ExpirationTimestamp { get; set; }
    }
}
