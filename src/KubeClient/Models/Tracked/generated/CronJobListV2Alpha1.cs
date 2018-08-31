using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CronJobList is a collection of cron jobs.
    /// </summary>
    [KubeListItem("CronJob", "batch/v2alpha1")]
    [KubeObject("CronJobList", "batch/v2alpha1")]
    public partial class CronJobListV2Alpha1 : Models.CronJobListV2Alpha1, ITracked
    {
        /// <summary>
        ///     items is the list of CronJobs.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.CronJobV2Alpha1> Items { get; } = new List<Models.CronJobV2Alpha1>();
    }
}
