using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDisruptionBudget is an object to define the max disruption that can be caused to a collection of pods
    /// </summary>
    [KubeObject("PodDisruptionBudget", "policy/v1beta1")]
    [KubeApi(KubeAction.List, "apis/policy/v1beta1/poddisruptionbudgets")]
    [KubeApi(KubeAction.WatchList, "apis/policy/v1beta1/watch/poddisruptionbudgets")]
    [KubeApi(KubeAction.List, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets")]
    [KubeApi(KubeAction.Create, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets")]
    [KubeApi(KubeAction.Get, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}")]
    [KubeApi(KubeAction.Delete, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}")]
    [KubeApi(KubeAction.Update, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/policy/v1beta1/watch/namespaces/{namespace}/poddisruptionbudgets")]
    [KubeApi(KubeAction.DeleteCollection, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets")]
    [KubeApi(KubeAction.Get, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/policy/v1beta1/watch/namespaces/{namespace}/poddisruptionbudgets/{name}")]
    [KubeApi(KubeAction.Patch, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/policy/v1beta1/namespaces/{namespace}/poddisruptionbudgets/{name}/status")]
    public partial class PodDisruptionBudgetV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the PodDisruptionBudget.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PodDisruptionBudgetSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the PodDisruptionBudget.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public PodDisruptionBudgetStatusV1Beta1 Status { get; set; }
    }
}
