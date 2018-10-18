using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenReviewStatus is the result of the token authentication request.
    /// </summary>
    public partial class TokenReviewStatusV1
    {
        /// <summary>
        ///     Authenticated indicates that the token was associated with a known user.
        /// </summary>
        [YamlMember(Alias = "authenticated")]
        [JsonProperty("authenticated", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Authenticated { get; set; }

        /// <summary>
        ///     Error indicates that the token couldn't be checked
        /// </summary>
        [YamlMember(Alias = "error")]
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        ///     User is the UserInfo associated with the provided token.
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public UserInfoV1 User { get; set; }
    }
}
