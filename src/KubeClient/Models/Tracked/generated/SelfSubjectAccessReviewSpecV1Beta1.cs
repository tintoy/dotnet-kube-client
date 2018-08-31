using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SelfSubjectAccessReviewSpec is a description of the access request.  Exactly one of ResourceAuthorizationAttributes and NonResourceAuthorizationAttributes must be set
    /// </summary>
    public partial class SelfSubjectAccessReviewSpecV1Beta1 : Models.SelfSubjectAccessReviewSpecV1Beta1, ITracked
    {
        /// <summary>
        ///     NonResourceAttributes describes information for a non-resource access request
        /// </summary>
        [JsonProperty("nonResourceAttributes")]
        [YamlMember(Alias = "nonResourceAttributes")]
        public override Models.NonResourceAttributesV1Beta1 NonResourceAttributes
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
        public override Models.ResourceAttributesV1Beta1 ResourceAttributes
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
