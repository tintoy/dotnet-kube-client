using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LocalSubjectAccessReview checks whether or not a user or group can perform an action in a given namespace. Having a namespace scoped resource makes it much easier to grant namespace scoped policy that includes permissions checking.
    /// </summary>
    [KubeObject("LocalSubjectAccessReview", "authorization.k8s.io/v1beta1")]
    [KubeApi(KubeAction.Create, "apis/authorization.k8s.io/v1beta1/namespaces/{namespace}/localsubjectaccessreviews")]
    public partial class LocalSubjectAccessReviewV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec holds information about the request being evaluated.  spec.namespace must be equal to the namespace you made the request against.  If empty, it is defaulted.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public SubjectAccessReviewSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is filled in by the server and indicates whether the request is allowed or not
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public SubjectAccessReviewStatusV1Beta1 Status { get; set; }
    }
}
