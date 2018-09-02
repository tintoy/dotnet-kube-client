using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudget is an object to define the max disruption that can be caused to a collection of pods
    /// </summary>
    [KubeObject("PodDisruptionBudget", "v1beta1")]
    [KubeApi("apis/policy/v1beta1/poddisruptionbudgets", KubeAction.List)]
    [KubeApi("apis/policy/v1beta1/watch/poddisruptionbudgets", KubeAction.WatchList)]
    [KubeApi("apis/policy/v1beta1/watch/namespaces/{namespace}/poddisruptionbudgets", KubeAction.WatchList)]
    [KubeApi("apis/policy/v1beta1/watch/namespaces/{namespace}/poddisruptionbudgets/{name}", KubeAction.Watch)]
    [KubeApi("apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    public partial class PodDisruptionBudgetV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the PodDisruptionBudget.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public PodDisruptionBudgetSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the PodDisruptionBudget.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public PodDisruptionBudgetStatusV1Beta1 Status { get; set; }
    }
}
