using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectAccessReviewSpec is a description of the access request.  Exactly one of ResourceAuthorizationAttributes and NonResourceAuthorizationAttributes must be set
    /// </summary>
    public partial class SubjectAccessReviewSpecV1Beta1
    {
        /// <summary>
        ///     Extra corresponds to the user.Info.GetExtra() method from the authenticator.  Since that is input to the authorizer it needs a reflection here.
        /// </summary>
        [YamlMember(Alias = "extra")]
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, List<string>> Extra { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     UID information about the requesting user.
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        /// <summary>
        ///     Groups is the groups you're testing for.
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Group { get; set; } = new List<string>();

        /// <summary>
        ///     User is the user you're testing for. If you specify "User" but not "Group", then is it interpreted as "What if User were not a member of any groups
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }

        /// <summary>
        ///     NonResourceAttributes describes information for a non-resource access request
        /// </summary>
        [YamlMember(Alias = "nonResourceAttributes")]
        [JsonProperty("nonResourceAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public NonResourceAttributesV1Beta1 NonResourceAttributes { get; set; }

        /// <summary>
        ///     ResourceAuthorizationAttributes describes information for a resource access request
        /// </summary>
        [YamlMember(Alias = "resourceAttributes")]
        [JsonProperty("resourceAttributes", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceAttributesV1Beta1 ResourceAttributes { get; set; }
    }
}
