using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenReviewSpec is a description of the token authentication request.
    /// </summary>
    [KubeResource("TokenReviewSpec", "v1beta1")]
    public class TokenReviewSpecV1Beta1
    {
        /// <summary>
        ///     Token is the opaque bearer token.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
