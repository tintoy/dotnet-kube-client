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
    [KubeApi("apis/batch/v1/jobs", KubeAction.List)]
    [KubeApi("apis/batch/v1/watch/jobs", KubeAction.WatchList)]
    [KubeApi("apis/batch/v1/watch/namespaces/{namespace}/jobs", KubeAction.WatchList)]
    [KubeApi("apis/batch/v1/watch/namespaces/{namespace}/jobs/{name}", KubeAction.Watch)]
    [KubeApi("apis/batch/v1/namespaces/{namespace}/jobs", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/batch/v1/namespaces/{namespace}/jobs/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("apis/batch/v1/namespaces/{namespace}/jobs/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
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
