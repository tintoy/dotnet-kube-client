using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobList is a collection of cron jobs.
    /// </summary>
    [KubeResource("CronJobList", "v2alpha1")]
    public class CronJobListV2Alpha1 : KubeResourceListV1
    {
        /// <summary>
        ///     items is the list of CronJobs.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<CronJobV2Alpha1> Items { get; set; } = new List<CronJobV2Alpha1>();
    }
}
