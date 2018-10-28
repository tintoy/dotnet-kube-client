using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Scale represents a scaling request for a resource.
    /// </summary>
    [KubeObject("Scale", "apps/v1beta1")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/scale")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}/scale")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/scale")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}/scale")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/scale")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/statefulsets/{name}/scale")]
    public partial class ScaleV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     defines the behavior of the scale. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ScaleSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     current status of the scale. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status. Read-only.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ScaleStatusV1Beta1 Status { get; set; }
    }
}
