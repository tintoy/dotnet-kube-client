using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectAccessReviewSpec is a description of the access request.  Exactly one of ResourceAuthorizationAttributes and NonResourceAuthorizationAttributes must be set
    /// </summary>
    public partial class SubjectAccessReviewSpecV1
    {
        /// <summary>
        ///     Extra corresponds to the user.Info.GetExtra() method from the authenticator.  Since that is input to the authorizer it needs a reflection here.
        /// </summary>
        [YamlMember(Alias = "extra")]
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Dictionary<string, List<string>> Extra { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     User is the user you're testing for. If you specify "User" but not "Groups", then is it interpreted as "What if User were not a member of any groups
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public virtual string User { get; set; }

        /// <summary>
        ///     Groups is the groups you're testing for.
        /// </summary>
        [YamlMember(Alias = "groups")]
        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Groups { get; set; } = new List<string>();

        /// <summary>
        ///     NonResourceAttributes describes information for a non-resource access request
        /// </summary>
        [JsonProperty("nonResourceAttributes")]
        [YamlMember(Alias = "nonResourceAttributes")]
        public virtual NonResourceAttributesV1 NonResourceAttributes { get; set; }

        /// <summary>
        ///     ResourceAuthorizationAttributes describes information for a resource access request
        /// </summary>
        [JsonProperty("resourceAttributes")]
        [YamlMember(Alias = "resourceAttributes")]
        public virtual ResourceAttributesV1 ResourceAttributes { get; set; }
    }
}
