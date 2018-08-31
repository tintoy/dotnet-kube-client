using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SubjectAccessReviewSpec is a description of the access request.  Exactly one of ResourceAuthorizationAttributes and NonResourceAuthorizationAttributes must be set
    /// </summary>
    public partial class SubjectAccessReviewSpecV1 : Models.SubjectAccessReviewSpecV1, ITracked
    {
        /// <summary>
        ///     Extra corresponds to the user.Info.GetExtra() method from the authenticator.  Since that is input to the authorizer it needs a reflection here.
        /// </summary>
        [YamlMember(Alias = "extra")]
        [JsonProperty("extra", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, List<string>> Extra { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     User is the user you're testing for. If you specify "User" but not "Groups", then is it interpreted as "What if User were not a member of any groups
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public override string User
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
        ///     Groups is the groups you're testing for.
        /// </summary>
        [YamlMember(Alias = "groups")]
        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Groups { get; set; } = new List<string>();

        /// <summary>
        ///     NonResourceAttributes describes information for a non-resource access request
        /// </summary>
        [JsonProperty("nonResourceAttributes")]
        [YamlMember(Alias = "nonResourceAttributes")]
        public override Models.NonResourceAttributesV1 NonResourceAttributes
        {
            get
            {
                return base.NonResourceAttributes;
            }
            set
            {
                base.NonResourceAttributes = value;

                __ModifiedProperties__.Add("NonResourceAttributes");
            }
        }


        /// <summary>
        ///     ResourceAuthorizationAttributes describes information for a resource access request
        /// </summary>
        [JsonProperty("resourceAttributes")]
        [YamlMember(Alias = "resourceAttributes")]
        public override Models.ResourceAttributesV1 ResourceAttributes
        {
            get
            {
                return base.ResourceAttributes;
            }
            set
            {
                base.ResourceAttributes = value;

                __ModifiedProperties__.Add("ResourceAttributes");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
