using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJob represents the configuration of a single cron job.
    /// </summary>
    [KubeObject("CronJob", "batch/v2alpha1")]
    [KubeApi(KubeAction.List, "apis/batch/v2alpha1/cronjobs")]
    [KubeApi(KubeAction.WatchList, "apis/batch/v2alpha1/watch/cronjobs")]
    [KubeApi(KubeAction.List, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs")]
    [KubeApi(KubeAction.Create, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs")]
    [KubeApi(KubeAction.Get, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}")]
    [KubeApi(KubeAction.Patch, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}")]
    [KubeApi(KubeAction.Delete, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}")]
    [KubeApi(KubeAction.Update, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/batch/v2alpha1/watch/namespaces/{namespace}/cronjobs")]
    [KubeApi(KubeAction.DeleteCollection, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs")]
    [KubeApi(KubeAction.Get, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/batch/v2alpha1/watch/namespaces/{namespace}/cronjobs/{name}")]
    [KubeApi(KubeAction.Patch, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/batch/v2alpha1/namespaces/{namespace}/cronjobs/{name}/status")]
    public partial class CronJobV2Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of a cron job, including the schedule. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public CronJobSpecV2Alpha1 Spec { get; set; }

        /// <summary>
        ///     Current status of a cron job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public CronJobStatusV2Alpha1 Status { get; set; }
    }
}
