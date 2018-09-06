using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Scale represents a scaling request for a resource.
    /// </summary>
    [KubeObject("Scale", "v1beta2")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/deployments/{name}/scale")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}/scale")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta2/namespaces/{namespace}/statefulsets/{name}/scale")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/deployments/{name}/scale")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}/scale")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta2/namespaces/{namespace}/statefulsets/{name}/scale")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/deployments/{name}/scale")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/replicasets/{name}/scale")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta2/namespaces/{namespace}/statefulsets/{name}/scale")]
    public partial class ScaleV1Beta2 : KubeResourceV1
    {
        /// <summary>
        ///     defines the behavior of the scale. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public ScaleSpecV1Beta2 Spec { get; set; }

        /// <summary>
        ///     current status of the scale. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status. Read-only.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public ScaleStatusV1Beta2 Status { get; set; }
    }
}
