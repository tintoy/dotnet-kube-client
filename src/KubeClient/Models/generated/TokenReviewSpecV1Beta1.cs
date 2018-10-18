using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenReviewSpec is a description of the token authentication request.
    /// </summary>
    public partial class TokenReviewSpecV1Beta1
    {
        /// <summary>
        ///     Token is the opaque bearer token.
        /// </summary>
        [YamlMember(Alias = "token")]
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }
}
