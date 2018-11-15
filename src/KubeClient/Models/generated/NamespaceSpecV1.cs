using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NamespaceSpec describes the attributes on a Namespace.
    /// </summary>
    public partial class NamespaceSpecV1
    {
        /// <summary>
        ///     Finalizers is an opaque list of values that must be empty to permanently remove object from storage. More info: https://kubernetes.io/docs/tasks/administer-cluster/namespaces/
        /// </summary>
        [YamlMember(Alias = "finalizers")]
        [JsonProperty("finalizers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Finalizers { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Finalizers"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeFinalizers() => Finalizers.Count > 0;
    }
}
