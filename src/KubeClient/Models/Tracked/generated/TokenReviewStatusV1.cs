using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     TokenReviewStatus is the result of the token authentication request.
    /// </summary>
    public partial class TokenReviewStatusV1 : Models.TokenReviewStatusV1, ITracked
    {
        /// <summary>
        ///     Authenticated indicates that the token was associated with a known user.
        /// </summary>
        [JsonProperty("authenticated")]
        [YamlMember(Alias = "authenticated")]
        public override bool Authenticated
        {
            get
            {
                return base.Authenticated;
            }
            set
            {
                base.Authenticated = value;

                __ModifiedProperties__.Add("Authenticated");
            }
        }


        /// <summary>
        ///     Error indicates that the token couldn't be checked
        /// </summary>
        [JsonProperty("error")]
        [YamlMember(Alias = "error")]
        public override string Error
        {
            get
            {
                return base.Error;
            }
            set
            {
                base.Error = value;

                __ModifiedProperties__.Add("Error");
            }
        }


        /// <summary>
        ///     User is the UserInfo associated with the provided token.
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public override Models.UserInfoV1 User
        {
            get
            {
                return base.User;
            }
            set
            {
                base.User = value;

                __ModifiedProperties__.Add("User");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
