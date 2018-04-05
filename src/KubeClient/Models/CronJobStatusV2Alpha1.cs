using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobStatus represents the current state of a cron job.
    /// </summary>
    public partial class CronJobStatusV2Alpha1
    {
        /// <summary>
        ///     Information when was the last time the job was successfully scheduled.
        /// </summary>
        [JsonProperty("lastScheduleTime")]
        public DateTime? LastScheduleTime { get; set; }

        /// <summary>
        ///     A list of pointers to currently running jobs.
        /// </summary>
        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public List<ObjectReferenceV1> Active { get; set; } = new List<ObjectReferenceV1>();
    }
}
