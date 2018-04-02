using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Job represents the configuration of a single job.
    /// </summary>
    [KubeObject("Job", "batch/v1")]
    public partial class JobV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of a job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public JobSpecV1 Spec { get; set; }

        /// <summary>
        ///     Current status of a job. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public JobStatusV1 Status { get; set; }
    }
}
