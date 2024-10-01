using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Overhead structure represents the resource overhead associated with running a pod.
    /// </summary>
    public partial class OverheadV1
    {
        /// <summary>
        ///     podFixed represents the fixed resource overhead associated with running a pod.
        /// </summary>
        [YamlMember(Alias = "podFixed")]
        [JsonProperty("podFixed", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> PodFixed { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="PodFixed"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePodFixed() => PodFixed.Count > 0;
    }
}
