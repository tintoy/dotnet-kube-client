using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TokenRequest contains parameters of a service account token.
    /// </summary>
    public partial class TokenRequestV1
    {
        /// <summary>
        ///     audience is the intended audience of the token in "TokenRequestSpec". It will default to the audiences of kube apiserver.
        /// </summary>
        [YamlMember(Alias = "audience")]
        [JsonProperty("audience", NullValueHandling = NullValueHandling.Include)]
        public string Audience { get; set; }

        /// <summary>
        ///     expirationSeconds is the duration of validity of the token in "TokenRequestSpec". It has the same default value of "ExpirationSeconds" in "TokenRequestSpec".
        /// </summary>
        [YamlMember(Alias = "expirationSeconds")]
        [JsonProperty("expirationSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExpirationSeconds { get; set; }
    }
}
