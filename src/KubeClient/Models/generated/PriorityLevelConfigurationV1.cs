using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityLevelConfiguration represents the configuration of a priority level.
    /// </summary>
    [KubeObject("PriorityLevelConfiguration", "flowcontrol.apiserver.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations")]
    [KubeApi(KubeAction.Create, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations")]
    [KubeApi(KubeAction.Get, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}")]
    [KubeApi(KubeAction.Delete, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}")]
    [KubeApi(KubeAction.Update, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/flowcontrol.apiserver.k8s.io/v1/watch/prioritylevelconfigurations")]
    [KubeApi(KubeAction.DeleteCollection, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations")]
    [KubeApi(KubeAction.Get, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/flowcontrol.apiserver.k8s.io/v1/watch/prioritylevelconfigurations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/flowcontrol.apiserver.k8s.io/v1/prioritylevelconfigurations/{name}/status")]
    public partial class PriorityLevelConfigurationV1 : KubeResourceV1
    {
        /// <summary>
        ///     `spec` is the specification of the desired behavior of a "request-priority". More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public PriorityLevelConfigurationSpecV1 Spec { get; set; }

        /// <summary>
        ///     `status` is the current status of a "request-priority". More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public PriorityLevelConfigurationStatusV1 Status { get; set; }
    }
}
