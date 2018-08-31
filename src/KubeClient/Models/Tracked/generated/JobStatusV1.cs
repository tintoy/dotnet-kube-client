using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JobStatus represents the current state of a Job.
    /// </summary>
    public partial class JobStatusV1 : Models.JobStatusV1, ITracked
    {
        /// <summary>
        ///     The number of pods which reached phase Failed.
        /// </summary>
        [JsonProperty("failed")]
        [YamlMember(Alias = "failed")]
        public override int Failed
        {
            get
            {
                return base.Failed;
            }
            set
            {
                base.Failed = value;

                __ModifiedProperties__.Add("Failed");
            }
        }


        /// <summary>
        ///     The number of pods which reached phase Succeeded.
        /// </summary>
        [JsonProperty("succeeded")]
        [YamlMember(Alias = "succeeded")]
        public override int Succeeded
        {
            get
            {
                return base.Succeeded;
            }
            set
            {
                base.Succeeded = value;

                __ModifiedProperties__.Add("Succeeded");
            }
        }


        /// <summary>
        ///     The number of actively running pods.
        /// </summary>
        [JsonProperty("active")]
        [YamlMember(Alias = "active")]
        public override int Active
        {
            get
            {
                return base.Active;
            }
            set
            {
                base.Active = value;

                __ModifiedProperties__.Add("Active");
            }
        }


        /// <summary>
        ///     Represents time when the job was completed. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [JsonProperty("completionTime")]
        [YamlMember(Alias = "completionTime")]
        public override DateTime? CompletionTime
        {
            get
            {
                return base.CompletionTime;
            }
            set
            {
                base.CompletionTime = value;

                __ModifiedProperties__.Add("CompletionTime");
            }
        }


        /// <summary>
        ///     Represents time when the job was acknowledged by the job controller. It is not guaranteed to be set in happens-before order across separate operations. It is represented in RFC3339 form and is in UTC.
        /// </summary>
        [JsonProperty("startTime")]
        [YamlMember(Alias = "startTime")]
        public override DateTime? StartTime
        {
            get
            {
                return base.StartTime;
            }
            set
            {
                base.StartTime = value;

                __ModifiedProperties__.Add("StartTime");
            }
        }


        /// <summary>
        ///     The latest available observations of an object's current state. More info: https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.JobConditionV1> Conditions { get; set; } = new List<Models.JobConditionV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
