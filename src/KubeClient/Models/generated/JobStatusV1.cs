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
        [YamlMember(Alias = "failed")]
        [JsonProperty("failed", NullValueHandling = NullValueHandling.Ignore)]
        public int? Failed { get; set; }

        /// <summary>
        ///     The number of pods which reached phase Succeeded.
        /// </summary>
        [YamlMember(Alias = "succeeded")]
        [JsonProperty("succeeded", NullValueHandling = NullValueHandling.Ignore)]
        public int? Succeeded { get; set; }

        /// <summary>
        ///     The number of actively running pods.
        /// </summary>
        [YamlMember(Alias = "active")]
        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public int? Active { get; set; }

        /// <summary>
        ///     Represents time when the job was completed. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [YamlMember(Alias = "completionTime")]
        [JsonProperty("completionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CompletionTime { get; set; }

        /// <summary>
        ///     Represents time when the job was acknowledged by the job controller. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [YamlMember(Alias = "startTime")]
        [JsonProperty("startTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        ///     The latest available observations of an object's current state. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<JobConditionV1> Conditions { get; } = new List<JobConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
