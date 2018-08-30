using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobStatus represents the current state of a Job.
    /// </summary>
    public partial class JobStatusV1
    {
        /// <summary>
        ///     The number of pods which reached phase Failed.
        /// </summary>
        [JsonProperty("failed")]
        [YamlMember(Alias = "failed")]
        public virtual int Failed { get; set; }

        /// <summary>
        ///     The number of pods which reached phase Succeeded.
        /// </summary>
        [JsonProperty("succeeded")]
        [YamlMember(Alias = "succeeded")]
        public virtual int Succeeded { get; set; }

        /// <summary>
        ///     The number of actively running pods.
        /// </summary>
        [JsonProperty("active")]
        [YamlMember(Alias = "active")]
        public virtual int Active { get; set; }

        /// <summary>
        ///     Represents time when the job was completed. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [JsonProperty("completionTime")]
        [YamlMember(Alias = "completionTime")]
        public virtual DateTime? CompletionTime { get; set; }

        /// <summary>
        ///     Represents time when the job was acknowledged by the job controller. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [JsonProperty("startTime")]
        [YamlMember(Alias = "startTime")]
        public virtual DateTime? StartTime { get; set; }

        /// <summary>
        ///     The latest available observations of an object's current state. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<JobConditionV1> Conditions { get; set; } = new List<JobConditionV1>();
    }
}
