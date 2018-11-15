using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED - This group version of Deployment is deprecated by apps/v1beta2/Deployment. See the release notes for more information. Deployment enables declarative updates for Pods and ReplicaSets.
    /// </summary>
    [KubeObject("Deployment", "apps/v1beta1")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta1/deployments")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta1/watch/deployments")]
    [KubeApi(KubeAction.List, "apis/apps/v1beta1/namespaces/{namespace}/deployments")]
    [KubeApi(KubeAction.Create, "apis/apps/v1beta1/namespaces/{namespace}/deployments")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}")]
    [KubeApi(KubeAction.Delete, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/apps/v1beta1/watch/namespaces/{namespace}/deployments")]
    [KubeApi(KubeAction.DeleteCollection, "apis/apps/v1beta1/namespaces/{namespace}/deployments")]
    [KubeApi(KubeAction.Get, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/apps/v1beta1/watch/namespaces/{namespace}/deployments/{name}")]
    [KubeApi(KubeAction.Patch, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/status")]
    public partial class DeploymentV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the Deployment.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public DeploymentSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the Deployment.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public DeploymentStatusV1Beta1 Status { get; set; }
    }
}
