using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobList is a collection of cron jobs.
    /// </summary>
    [KubeListItem("CronJob", "batch/v1")]
    [KubeObject("CronJobList", "batch/v1")]
    public partial class CronJobListV1 : KubeResourceListV1<CronJobV1>
    {
        /// <summary>
        ///     items is the list of CronJobs.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CronJobV1> Items { get; } = new List<CronJobV1>();
    }
}
