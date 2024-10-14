using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobSpec describes how the job execution will look like and when it will actually run.
    /// </summary>
    public partial class CronJobSpecV1
    {
        /// <summary>
        ///     This flag tells the controller to suspend subsequent executions, it does not apply to already started executions.  Defaults to false.
        /// </summary>
        [YamlMember(Alias = "suspend")]
        [JsonProperty("suspend", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Suspend { get; set; }

        /// <summary>
        ///     Specifies the job that will be created when executing a CronJob.
        /// </summary>
        [YamlMember(Alias = "jobTemplate")]
        [JsonProperty("jobTemplate", NullValueHandling = NullValueHandling.Include)]
        public JobTemplateSpecV1 JobTemplate { get; set; }

        /// <summary>
        ///     The schedule in Cron format, see https://en.wikipedia.org/wiki/Cron.
        /// </summary>
        [YamlMember(Alias = "schedule")]
        [JsonProperty("schedule", NullValueHandling = NullValueHandling.Include)]
        public string Schedule { get; set; }

        /// <summary>
        ///     The time zone name for the given schedule, see https://en.wikipedia.org/wiki/List_of_tz_database_time_zones. If not specified, this will default to the time zone of the kube-controller-manager process. The set of valid time zone names and the time zone offset is loaded from the system-wide time zone database by the API server during CronJob validation and the controller manager during execution. If no system-wide time zone database can be found a bundled version of the database is used instead. If the time zone name becomes invalid during the lifetime of a CronJob or due to a change in host configuration, the controller will stop creating new new Jobs and will create a system event with the reason UnknownTimeZone. More information can be found in https://kubernetes.io/docs/concepts/workloads/controllers/cron-jobs/#time-zones
        /// </summary>
        [YamlMember(Alias = "timeZone")]
        [JsonProperty("timeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        /// <summary>
        ///     Optional deadline in seconds for starting the job if it misses scheduled time for any reason.  Missed jobs executions will be counted as failed ones.
        /// </summary>
        [YamlMember(Alias = "startingDeadlineSeconds")]
        [JsonProperty("startingDeadlineSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? StartingDeadlineSeconds { get; set; }

        /// <summary>
        ///     The number of failed finished jobs to retain. Value must be non-negative integer. Defaults to 1.
        /// </summary>
        [YamlMember(Alias = "failedJobsHistoryLimit")]
        [JsonProperty("failedJobsHistoryLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? FailedJobsHistoryLimit { get; set; }

        /// <summary>
        ///     The number of successful finished jobs to retain. Value must be non-negative integer. Defaults to 3.
        /// </summary>
        [YamlMember(Alias = "successfulJobsHistoryLimit")]
        [JsonProperty("successfulJobsHistoryLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? SuccessfulJobsHistoryLimit { get; set; }

        /// <summary>
        ///     Specifies how to treat concurrent executions of a Job. Valid values are:
        ///     
        ///     - "Allow" (default): allows CronJobs to run concurrently; - "Forbid": forbids concurrent runs, skipping next run if previous run hasn't finished yet; - "Replace": cancels currently running job and replaces it with a new one
        /// </summary>
        [YamlMember(Alias = "concurrencyPolicy")]
        [JsonProperty("concurrencyPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConcurrencyPolicy { get; set; }
    }
}
