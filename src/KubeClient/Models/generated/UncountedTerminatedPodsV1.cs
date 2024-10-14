using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     UncountedTerminatedPods holds UIDs of Pods that have terminated but haven't been accounted in Job status counters.
    /// </summary>
    public partial class UncountedTerminatedPodsV1
    {
        /// <summary>
        ///     failed holds UIDs of failed Pods.
        /// </summary>
        [YamlMember(Alias = "failed")]
        [JsonProperty("failed", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Failed { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Failed"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeFailed() => Failed.Count > 0;

        /// <summary>
        ///     succeeded holds UIDs of succeeded Pods.
        /// </summary>
        [YamlMember(Alias = "succeeded")]
        [JsonProperty("succeeded", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Succeeded { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Succeeded"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSucceeded() => Succeeded.Count > 0;
    }
}
