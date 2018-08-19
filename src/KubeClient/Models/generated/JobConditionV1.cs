using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobCondition describes current state of a job.
    /// </summary>
    public partial class JobConditionV1
    {
        /// <summary>
        ///     Last time the condition was checked.
        /// </summary>
        [JsonProperty("lastProbeTime")]
        public DateTime? LastProbeTime { get; set; }

        /// <summary>
        ///     Last time the condition transit from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     Human readable message indicating details about last transition.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Type of job condition, Complete or Failed.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     (brief) reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
