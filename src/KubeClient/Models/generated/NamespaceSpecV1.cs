using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NamespaceSpec describes the attributes on a Namespace.
    /// </summary>
    public partial class NamespaceSpecV1
    {
        /// <summary>
        ///     Finalizers is an opaque list of values that must be empty to permanently remove object from storage. More info: https://git.k8s.io/community/contributors/design-proposals/namespaces.md#finalizers
        /// </summary>
        [JsonProperty("finalizers", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Finalizers { get; set; } = new List<string>();
    }
}
