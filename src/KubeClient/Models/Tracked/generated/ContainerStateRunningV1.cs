using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ContainerStateRunning is a running state of a container.
    /// </summary>
    public partial class ContainerStateRunningV1 : Models.ContainerStateRunningV1, ITracked
    {
        /// <summary>
        ///     Time at which the container was last (re-)started
        /// </summary>
        [JsonProperty("startedAt")]
        [YamlMember(Alias = "startedAt")]
        public override DateTime? StartedAt
        {
            get
            {
                return base.StartedAt;
            }
            set
            {
                base.StartedAt = value;

                __ModifiedProperties__.Add("StartedAt");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
