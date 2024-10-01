using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowSchema defines the schema of a group of flows. Note that a flow is made up of a set of inbound API requests with similar attributes and is identified by a pair of strings: the name of the FlowSchema and a "flow distinguisher".
    /// </summary>
    [KubeObject("FlowSchema", "flowcontrol.apiserver.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas")]
    [KubeApi(KubeAction.Create, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas")]
    [KubeApi(KubeAction.Get, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}")]
    [KubeApi(KubeAction.Patch, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}")]
    [KubeApi(KubeAction.Delete, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}")]
    [KubeApi(KubeAction.Update, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/flowcontrol.apiserver.k8s.io/v1/watch/flowschemas")]
    [KubeApi(KubeAction.DeleteCollection, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas")]
    [KubeApi(KubeAction.Get, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/flowcontrol.apiserver.k8s.io/v1/watch/flowschemas/{name}")]
    [KubeApi(KubeAction.Patch, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/flowcontrol.apiserver.k8s.io/v1/flowschemas/{name}/status")]
    public partial class FlowSchemaV1 : KubeResourceV1
    {
        /// <summary>
        ///     `spec` is the specification of the desired behavior of a FlowSchema. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public FlowSchemaSpecV1 Spec { get; set; }

        /// <summary>
        ///     `status` is the current status of a FlowSchema. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public FlowSchemaStatusV1 Status { get; set; }
    }
}
