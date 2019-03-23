using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobSpec describes how the job execution will look like and when it will actually run.
    /// </summary>
    public partial class CronJobSpecV2Alpha1
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
        public JobTemplateSpecV2Alpha1 JobTemplate { get; set; }

        /// <summary>
        ///     The schedule in Cron format, see https://en.wikipedia.org/wiki/Cron.
        /// </summary>
        [YamlMember(Alias = "schedule")]
        [JsonProperty("schedule", NullValueHandling = NullValueHandling.Include)]
        public string Schedule { get; set; }

        /// <summary>
        ///     Optional deadline in seconds for starting the job if it misses scheduled time for any reason.  Missed jobs executions will be counted as failed ones.
        /// </summary>
        [YamlMember(Alias = "startingDeadlineSeconds")]
        [JsonProperty("startingDeadlineSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public long? StartingDeadlineSeconds { get; set; }

        /// <summary>
        ///     The number of failed finished jobs to retain. This is a pointer to distinguish between explicit zero and not specified.
        /// </summary>
        [YamlMember(Alias = "failedJobsHistoryLimit")]
        [JsonProperty("failedJobsHistoryLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? FailedJobsHistoryLimit { get; set; }

        /// <summary>
        ///     The number of successful finished jobs to retain. This is a pointer to distinguish between explicit zero and not specified.
        /// </summary>
        [YamlMember(Alias = "successfulJobsHistoryLimit")]
        [JsonProperty("successfulJobsHistoryLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int? SuccessfulJobsHistoryLimit { get; set; }

        /// <summary>
        ///     Specifies how to treat concurrent executions of a Job. Valid values are: - "Allow" (default): allows CronJobs to run concurrently; - "Forbid": forbids concurrent runs, skipping next run if previous run hasn't finished yet; - "Replace": cancels currently running job and replaces it with a new one
        /// </summary>
        [YamlMember(Alias = "concurrencyPolicy")]
        [JsonProperty("concurrencyPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConcurrencyPolicy { get; set; }
    }
}
