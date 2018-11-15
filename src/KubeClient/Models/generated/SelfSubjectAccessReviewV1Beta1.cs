using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SelfSubjectAccessReview checks whether or the current user can perform an action.  Not filling in a spec.namespace means "in all namespaces".  Self is a special case, because users should always be able to check whether they can perform an action
    /// </summary>
    [KubeObject("SelfSubjectAccessReview", "authorization.k8s.io/v1beta1")]
    [KubeApi(KubeAction.Create, "apis/authorization.k8s.io/v1beta1/selfsubjectaccessreviews")]
    public partial class SelfSubjectAccessReviewV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec holds information about the request being evaluated.  user and groups must be empty
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public SelfSubjectAccessReviewSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is filled in by the server and indicates whether the request is allowed or not
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public SubjectAccessReviewStatusV1Beta1 Status { get; set; }
    }
}
