using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SelfSubjectReview contains the user information that the kube-apiserver has about the user making this request. When using impersonation, users will receive the user info of the user being impersonated.  If impersonation or request header authentication is used, any extra keys will have their case ignored and returned as lowercase.
    /// </summary>
    [KubeObject("SelfSubjectReview", "authentication.k8s.io/v1beta1")]
    [KubeApi(KubeAction.Create, "apis/authentication.k8s.io/v1beta1/selfsubjectreviews")]
    public partial class SelfSubjectReviewV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Status is filled in by the server with the user attributes.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public SelfSubjectReviewStatusV1Beta1 Status { get; set; }
    }
}
