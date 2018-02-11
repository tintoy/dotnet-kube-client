using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobList is a collection of jobs.
    /// </summary>
    [KubeResource("JobList", "v1")]
    public class JobListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     items is the list of Jobs.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<JobV1> Items { get; set; } = new List<JobV1>();
    }
}
