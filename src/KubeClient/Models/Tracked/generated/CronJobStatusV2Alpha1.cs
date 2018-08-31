using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CronJobStatus represents the current state of a cron job.
    /// </summary>
    public partial class CronJobStatusV2Alpha1 : Models.CronJobStatusV2Alpha1
    {
        /// <summary>
        ///     A list of pointers to currently running jobs.
        /// </summary>
        [YamlMember(Alias = "active")]
        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.ObjectReferenceV1> Active { get; set; } = new List<Models.ObjectReferenceV1>();

        /// <summary>
        ///     Information when was the last time the job was successfully scheduled.
        /// </summary>
        [JsonProperty("lastScheduleTime")]
        [YamlMember(Alias = "lastScheduleTime")]
        public override DateTime? LastScheduleTime
        {
            get
            {
                return base.LastScheduleTime;
            }
            set
            {
                base.LastScheduleTime = value;

                __ModifiedProperties__.Add("LastScheduleTime");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
