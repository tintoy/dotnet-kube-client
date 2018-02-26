using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     UserInfo holds the information about the user needed to implement the user.Info interface.
    /// </summary>
    [KubeObject("UserInfo", "v1beta1")]
    public class UserInfoV1Beta1
    {
        /// <summary>
        ///     Any additional information provided by the authenticator.
        /// </summary>
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<string>> Extra { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     A unique value that identifies this user across time. If this user is deleted and another user by the same name is added, they will have different UIDs.
        /// </summary>
        [JsonProperty("uid")]
        public string Uid { get; set; }

        /// <summary>
        ///     The name that uniquely identifies this user among all active users.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        ///     The names of groups this user is a part of.
        /// </summary>
        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Groups { get; set; } = new List<string>();
    }
}
