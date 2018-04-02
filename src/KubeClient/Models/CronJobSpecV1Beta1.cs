using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     CronJobSpec describes how the job execution will look like and when it will actually run.
    /// </summary>
    [KubeObject("CronJobSpec", "v1beta1")]
    public partial class CronJobSpecV1Beta1
    {
        /// <summary>
        ///     This flag tells the controller to suspend subsequent executions, it does not apply to already started executions.  Defaults to false.
        /// </summary>
        [JsonProperty("suspend")]
        public bool Suspend { get; set; }

        /// <summary>
        ///     Specifies the job that will be created when executing a CronJob.
        /// </summary>
        [JsonProperty("jobTemplate")]
        public JobTemplateSpecV1Beta1 JobTemplate { get; set; }

        /// <summary>
        ///     The schedule in Cron format, see https://en.wikipedia.org/wiki/Cron.
        /// </summary>
        [JsonProperty("schedule")]
        public string Schedule { get; set; }

        /// <summary>
        ///     Optional deadline in seconds for starting the job if it misses scheduled time for any reason.  Missed jobs executions will be counted as failed ones.
        /// </summary>
        [JsonProperty("startingDeadlineSeconds")]
        public int? StartingDeadlineSeconds { get; set; }

        /// <summary>
        ///     The number of failed finished jobs to retain. This is a pointer to distinguish between explicit zero and not specified. Defaults to 1.
        /// </summary>
        [JsonProperty("failedJobsHistoryLimit")]
        public int FailedJobsHistoryLimit { get; set; }

        /// <summary>
        ///     The number of successful finished jobs to retain. This is a pointer to distinguish between explicit zero and not specified. Defaults to 3.
        /// </summary>
        [JsonProperty("successfulJobsHistoryLimit")]
        public int SuccessfulJobsHistoryLimit { get; set; }

        /// <summary>
        ///     Specifies how to treat concurrent executions of a Job. Defaults to Allow.
        /// </summary>
        [JsonProperty("concurrencyPolicy")]
        public string ConcurrencyPolicy { get; set; }
    }
}
