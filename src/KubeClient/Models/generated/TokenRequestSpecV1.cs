using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenRequestSpec contains client provided parameters of a token request.
    /// </summary>
    public partial class TokenRequestSpecV1
    {
        /// <summary>
        ///     BoundObjectRef is a reference to an object that the token will be bound to. The token will only be valid for as long as the bound object exists. NOTE: The API server's TokenReview endpoint will validate the BoundObjectRef, but other audiences may not. Keep ExpirationSeconds small if you want prompt revocation.
        /// </summary>
        [YamlMember(Alias = "boundObjectRef")]
        [JsonProperty("boundObjectRef", NullValueHandling = NullValueHandling.Ignore)]
        public BoundObjectReferenceV1 BoundObjectRef { get; set; }

        /// <summary>
        ///     Audiences are the intendend audiences of the token. A recipient of a token must identify themself with an identifier in the list of audiences of the token, and otherwise should reject the token. A token issued for multiple audiences may be used to authenticate against any of the audiences listed but implies a high degree of trust between the target audiences.
        /// </summary>
        [YamlMember(Alias = "audiences")]
        [JsonProperty("audiences", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Audiences { get; } = new List<string>();

        /// <summary>
        ///     ExpirationSeconds is the requested duration of validity of the request. The token issuer may return a token with a different validity duration so a client needs to check the 'expiration' field in a response.
        /// </summary>
        [YamlMember(Alias = "expirationSeconds")]
        [JsonProperty("expirationSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpirationSeconds { get; set; }
    }
}