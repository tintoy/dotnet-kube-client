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

        /// <summary>
        ///     Audiences are audience identifiers chosen by the authenticator that are compatible with both the TokenReview and token. An identifier is any identifier in the intersection of the TokenReviewSpec audiences and the token's audiences. A client of the TokenReview API that sets the spec.audiences field should validate that a compatible audience identifier is returned in the status.audiences field to ensure that the TokenReview server is audience aware. If a TokenReview returns an empty status.audience field where status.authenticated is "true", the token is valid against the audience of the Kubernetes API server.
        /// </summary>
        [YamlMember(Alias = "audiences")]
        [JsonProperty("audiences", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Audiences { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Audiences"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAudiences() => Audiences.Count > 0;
    }
}
