using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NamespaceStatus is information about the current status of a Namespace.
    /// </summary>
    public partial class NamespaceStatusV1
    {
        /// <summary>
        ///     Phase is the current lifecycle phase of the namespace. More info: https://kubernetes.io/docs/tasks/administer-cluster/namespaces/
        /// </summary>
        [YamlMember(Alias = "phase")]
        [JsonProperty("phase", NullValueHandling = NullValueHandling.Ignore)]
        public string Phase { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a namespace's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NamespaceConditionV1> Conditions { get; } = new List<NamespaceConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
