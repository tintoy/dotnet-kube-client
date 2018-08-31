using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     TokenReview attempts to authenticate a token to a known user. Note: TokenReview requests may be cached by the webhook token authenticator plugin in the kube-apiserver.
    /// </summary>
    [KubeObject("TokenReview", "authentication.k8s.io/v1beta1")]
    public partial class TokenReviewV1Beta1 : Models.TokenReviewV1Beta1, ITracked
    {
        /// <summary>
        ///     Spec holds information about the request being evaluated
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.TokenReviewSpecV1Beta1 Spec
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
        ///     Status is filled in by the server and indicates whether the request can be authenticated.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override Models.TokenReviewStatusV1Beta1 Status
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
