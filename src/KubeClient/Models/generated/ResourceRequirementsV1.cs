using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceRequirements describes the compute resource requirements.
    /// </summary>
    public partial class ResourceRequirementsV1
    {
        /// <summary>
        ///     Limits describes the maximum amount of compute resources allowed. More info: https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
        /// </summary>
        [YamlMember(Alias = "limits")]
        [JsonProperty("limits", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Limits { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Limits"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeLimits() => Limits.Count > 0;

        /// <summary>
        ///     Requests describes the minimum amount of compute resources required. If Requests is omitted for a container, it defaults to Limits if that is explicitly specified, otherwise to an implementation-defined value. More info: https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
        /// </summary>
        [YamlMember(Alias = "requests")]
        [JsonProperty("requests", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Requests { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Requests"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRequests() => Requests.Count > 0;
    }
}
