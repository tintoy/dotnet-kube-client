using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Namespace provides a scope for Names. Use of multiple namespaces is optional.
    /// </summary>
    [KubeObject("Namespace", "v1")]
    public partial class NamespaceV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the behavior of the Namespace. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public NamespaceSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status describes the current status of a Namespace. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public NamespaceStatusV1 Status { get; set; }
    }
}
