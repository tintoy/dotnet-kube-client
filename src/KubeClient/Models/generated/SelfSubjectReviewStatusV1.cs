using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SelfSubjectReviewStatus is filled by the kube-apiserver and sent back to a user.
    /// </summary>
    public partial class SelfSubjectReviewStatusV1
    {
        /// <summary>
        ///     User attributes of the user making this request.
        /// </summary>
        [YamlMember(Alias = "userInfo")]
        [JsonProperty("userInfo", NullValueHandling = NullValueHandling.Ignore)]
        public UserInfoV1 UserInfo { get; set; }
    }
}
