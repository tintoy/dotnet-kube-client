using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     LocalSubjectAccessReview checks whether or not a user or group can perform an action in a given namespace. Having a namespace scoped resource makes it much easier to grant namespace scoped policy that includes permissions checking.
    /// </summary>
    [KubeObject("LocalSubjectAccessReview", "authorization.k8s.io/v1")]
    public partial class LocalSubjectAccessReviewV1 : Models.LocalSubjectAccessReviewV1, ITracked
    {
        /// <summary>
        ///     Spec holds information about the request being evaluated.  spec.namespace must be equal to the namespace you made the request against.  If empty, it is defaulted.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.SubjectAccessReviewSpecV1 Spec
        {
            get
            {
                return base.Spec;
            }
            set
            {
                base.Spec = value;

                __ModifiedProperties__.Add("Spec");
            }
        }


        /// <summary>
        ///     Status is filled in by the server and indicates whether the request is allowed or not
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override Models.SubjectAccessReviewStatusV1 Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;

                __ModifiedProperties__.Add("Status");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
