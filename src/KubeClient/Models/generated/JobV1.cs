using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Job represents the configuration of a single job.
    /// </summary>
    [KubeObject("Job", "v1")]
    [KubeApi(KubeAction.List, "apis/batch/v1/jobs")]
    [KubeApi(KubeAction.WatchList, "apis/batch/v1/watch/jobs")]
    [KubeApi(KubeAction.List, "apis/batch/v1/namespaces/{namespace}/jobs")]
    [KubeApi(KubeAction.Create, "apis/batch/v1/namespaces/{namespace}/jobs")]
    [KubeApi(KubeAction.Get, "apis/batch/v1/namespaces/{namespace}/jobs/{name}")]
    [KubeApi(KubeAction.Patch, "apis/batch/v1/namespaces/{namespace}/jobs/{name}")]
    [KubeApi(KubeAction.Delete, "apis/batch/v1/namespaces/{namespace}/jobs/{name}")]
    [KubeApi(KubeAction.Update, "apis/batch/v1/namespaces/{namespace}/jobs/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/batch/v1/watch/namespaces/{namespace}/jobs")]
    [KubeApi(KubeAction.DeleteCollection, "apis/batch/v1/namespaces/{namespace}/jobs")]
    [KubeApi(KubeAction.Get, "apis/batch/v1/namespaces/{namespace}/jobs/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/batch/v1/watch/namespaces/{namespace}/jobs/{name}")]
    [KubeApi(KubeAction.Patch, "apis/batch/v1/namespaces/{namespace}/jobs/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/batch/v1/namespaces/{namespace}/jobs/{name}/status")]
    public partial class JobV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of a job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public JobSpecV1 Spec { get; set; }

        /// <summary>
        ///     Current status of a job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public JobStatusV1 Status { get; set; }
    }
}
