using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     TokenReviewSpec is a description of the token authentication request.
    /// </summary>
    public partial class TokenReviewSpecV1 : Models.TokenReviewSpecV1, ITracked
    {
        /// <summary>
        ///     Token is the opaque bearer token.
        /// </summary>
        [JsonProperty("token")]
        [YamlMember(Alias = "token")]
        public override string Token
        {
            get
            {
                return base.Token;
            }
            set
            {
                base.Token = value;

                __ModifiedProperties__.Add("Token");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
