using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenReviewSpec is a description of the token authentication request.
    /// </summary>
    public partial class TokenReviewSpecV1
    {
        /// <summary>
        ///     Token is the opaque bearer token.
        /// </summary>
        [JsonProperty("token")]
        [YamlMember(Alias = "token")]
        public virtual string Token { get; set; }
    }
}
