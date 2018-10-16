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
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public string Phase { get; set; }
    }
}
