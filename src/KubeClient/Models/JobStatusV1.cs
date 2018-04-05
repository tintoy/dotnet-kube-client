using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobStatus represents the current state of a Job.
    /// </summary>
    public partial class JobStatusV1
    {
        /// <summary>
        ///     The latest available observations of an object's current state. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<JobConditionV1> Conditions { get; set; } = new List<JobConditionV1>();

        /// <summary>
        ///     Represents time when the job was completed. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [JsonProperty("completionTime")]
        public DateTime? CompletionTime { get; set; }

        /// <summary>
        ///     Represents time when the job was acknowledged by the job controller. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        ///     The number of pods which reached phase Failed.
        /// </summary>
        [JsonProperty("failed")]
        public int Failed { get; set; }

        /// <summary>
        ///     The number of actively running pods.
        /// </summary>
        [JsonProperty("active")]
        public int Active { get; set; }

        /// <summary>
        ///     The number of pods which reached phase Succeeded.
        /// </summary>
        [JsonProperty("succeeded")]
        public int Succeeded { get; set; }
    }
}
