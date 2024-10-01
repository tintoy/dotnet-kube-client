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
        [YamlMember(Alias = "token")]
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>
        ///     Audiences is a list of the identifiers that the resource server presented with the token identifies as. Audience-aware token authenticators will verify that the token was intended for at least one of the audiences in this list. If no audiences are provided, the audience will default to the audience of the Kubernetes apiserver.
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
