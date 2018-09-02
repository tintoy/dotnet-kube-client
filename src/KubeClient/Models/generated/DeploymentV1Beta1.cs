using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Deployment enables declarative updates for Pods and ReplicaSets.
    /// </summary>
    [KubeObject("Deployment", "v1beta1")]
    [KubeApi("apis/apps/v1beta1/deployments", KubeAction.List)]
    [KubeApi("apis/apps/v1beta1/watch/deployments", KubeAction.WatchList)]
    [KubeApi("apis/apps/v1beta1/watch/namespaces/{namespace}/deployments", KubeAction.WatchList)]
    [KubeApi("apis/apps/v1beta1/watch/namespaces/{namespace}/deployments/{name}", KubeAction.Watch)]
    [KubeApi("apis/apps/v1beta1/namespaces/{namespace}/deployments", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("apis/apps/v1beta1/namespaces/{namespace}/deployments/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    public partial class DeploymentV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the Deployment.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public DeploymentSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the Deployment.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public DeploymentStatusV1Beta1 Status { get; set; }
    }
}
