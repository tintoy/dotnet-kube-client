using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenReviewStatus is the result of the token authentication request.
    /// </summary>
    public partial class TokenReviewStatusV1Beta1
    {
        /// <summary>
        ///     Authenticated indicates that the token was associated with a known user.
        /// </summary>
        [JsonProperty("authenticated")]
        [YamlMember(Alias = "authenticated")]
        public bool Authenticated { get; set; }

        /// <summary>
        ///     Error indicates that the token couldn't be checked
        /// </summary>
        [JsonProperty("error")]
        [YamlMember(Alias = "error")]
        public string Error { get; set; }

        /// <summary>
        ///     User is the UserInfo associated with the provided token.
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public UserInfoV1Beta1 User { get; set; }
    }
}
