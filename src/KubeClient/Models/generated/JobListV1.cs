using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JobList is a collection of jobs.
    /// </summary>
    [KubeListItem("Job", "batch/v1")]
    [KubeObject("JobList", "batch/v1")]
    public partial class JobListV1 : KubeResourceListV1<JobV1>
    {
        /// <summary>
        ///     items is the list of Jobs.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<JobV1> Items { get; } = new List<JobV1>();
    }
}
