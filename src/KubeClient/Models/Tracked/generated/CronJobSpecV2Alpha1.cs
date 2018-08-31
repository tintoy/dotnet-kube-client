using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CronJobSpec describes how the job execution will look like and when it will actually run.
    /// </summary>
    public partial class CronJobSpecV2Alpha1 : Models.CronJobSpecV2Alpha1, ITracked
    {
        /// <summary>
        ///     This flag tells the controller to suspend subsequent executions, it does not apply to already started executions.  Defaults to false.
        /// </summary>
        [JsonProperty("suspend")]
        [YamlMember(Alias = "suspend")]
        public override bool Suspend
        {
            get
            {
                return base.Suspend;
            }
            set
            {
                base.Suspend = value;

                __ModifiedProperties__.Add("Suspend");
            }
        }


        /// <summary>
        ///     Specifies the job that will be created when executing a CronJob.
        /// </summary>
        [JsonProperty("jobTemplate")]
        [YamlMember(Alias = "jobTemplate")]
        public override Models.JobTemplateSpecV2Alpha1 JobTemplate
        {
            get
            {
                return base.JobTemplate;
            }
            set
            {
                base.JobTemplate = value;

                __ModifiedProperties__.Add("JobTemplate");
            }
        }


        /// <summary>
        ///     The schedule in Cron format, see https://en.wikipedia.org/wiki/Cron.
        /// </summary>
        [JsonProperty("schedule")]
        [YamlMember(Alias = "schedule")]
        public override string Schedule
        {
            get
            {
                return base.Schedule;
            }
            set
            {
                base.Schedule = value;

                __ModifiedProperties__.Add("Schedule");
            }
        }


        /// <summary>
        ///     Optional deadline in seconds for starting the job if it misses scheduled time for any reason.  Missed jobs executions will be counted as failed ones.
        /// </summary>
        [JsonProperty("startingDeadlineSeconds")]
        [YamlMember(Alias = "startingDeadlineSeconds")]
        public override int? StartingDeadlineSeconds
        {
            get
            {
                return base.StartingDeadlineSeconds;
            }
            set
            {
                base.StartingDeadlineSeconds = value;

                __ModifiedProperties__.Add("StartingDeadlineSeconds");
            }
        }


        /// <summary>
        ///     The number of failed finished jobs to retain. This is a pointer to distinguish between explicit zero and not specified.
        /// </summary>
        [JsonProperty("failedJobsHistoryLimit")]
        [YamlMember(Alias = "failedJobsHistoryLimit")]
        public override int FailedJobsHistoryLimit
        {
            get
            {
                return base.FailedJobsHistoryLimit;
            }
            set
            {
                base.FailedJobsHistoryLimit = value;

                __ModifiedProperties__.Add("FailedJobsHistoryLimit");
            }
        }


        /// <summary>
        ///     The number of successful finished jobs to retain. This is a pointer to distinguish between explicit zero and not specified.
        /// </summary>
        [JsonProperty("successfulJobsHistoryLimit")]
        [YamlMember(Alias = "successfulJobsHistoryLimit")]
        public override int SuccessfulJobsHistoryLimit
        {
            get
            {
                return base.SuccessfulJobsHistoryLimit;
            }
            set
            {
                base.SuccessfulJobsHistoryLimit = value;

                __ModifiedProperties__.Add("SuccessfulJobsHistoryLimit");
            }
        }


        /// <summary>
        ///     Specifies how to treat concurrent executions of a Job. Defaults to Allow.
        /// </summary>
        [JsonProperty("concurrencyPolicy")]
        [YamlMember(Alias = "concurrencyPolicy")]
        public override string ConcurrencyPolicy
        {
            get
            {
                return base.ConcurrencyPolicy;
            }
            set
            {
                base.ConcurrencyPolicy = value;

                __ModifiedProperties__.Add("ConcurrencyPolicy");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
