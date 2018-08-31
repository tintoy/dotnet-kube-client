using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ResourceRequirements describes the compute resource requirements.
    /// </summary>
    public partial class ResourceRequirementsV1 : Models.ResourceRequirementsV1, ITracked
    {
        /// <summary>
        ///     Limits describes the maximum amount of compute resources allowed. More info: https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
        /// </summary>
        [YamlMember(Alias = "limits")]
        [JsonProperty("limits", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Limits { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Requests describes the minimum amount of compute resources required. If Requests is omitted for a container, it defaults to Limits if that is explicitly specified, otherwise to an implementation-defined value. More info: https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
        /// </summary>
        [YamlMember(Alias = "requests")]
        [JsonProperty("requests", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Requests { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
