using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JobList is a collection of jobs.
    /// </summary>
    [KubeListItem("Job", "batch/v1")]
    [KubeObject("JobList", "batch/v1")]
    public partial class JobListV1 : Models.JobListV1, ITracked
    {
        /// <summary>
        ///     items is the list of Jobs.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.JobV1> Items { get; } = new List<Models.JobV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
