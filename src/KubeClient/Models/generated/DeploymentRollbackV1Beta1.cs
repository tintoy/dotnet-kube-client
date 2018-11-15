using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED. DeploymentRollback stores the information required to rollback a deployment.
    /// </summary>
    public partial class DeploymentRollbackV1Beta1 : KubeObjectV1
    {
        /// <summary>
        ///     Required: This must match the Name of a deployment.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     The config of this deployment rollback.
        /// </summary>
        [YamlMember(Alias = "rollbackTo")]
        [JsonProperty("rollbackTo", NullValueHandling = NullValueHandling.Include)]
        public RollbackConfigV1Beta1 RollbackTo { get; set; }

        /// <summary>
        ///     The annotations to be updated to a deployment
        /// </summary>
        [YamlMember(Alias = "updatedAnnotations")]
        [JsonProperty("updatedAnnotations", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> UpdatedAnnotations { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="UpdatedAnnotations"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeUpdatedAnnotations() => UpdatedAnnotations.Count > 0;
    }
}
