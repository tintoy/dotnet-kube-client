using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobList is a collection of cron jobs.
    /// </summary>
    [KubeListItem("CronJob", "batch/v1beta1")]
    [KubeObject("CronJobList", "batch/v1beta1")]
    public partial class CronJobListV1Beta1 : KubeResourceListV1<CronJobV1Beta1>
    {
        /// <summary>
        ///     items is the list of CronJobs.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CronJobV1Beta1> Items { get; } = new List<CronJobV1Beta1>();
    }
}
