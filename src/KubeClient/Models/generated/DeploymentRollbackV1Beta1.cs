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
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     The config of this deployment rollback.
        /// </summary>
        [JsonProperty("rollbackTo")]
        [YamlMember(Alias = "rollbackTo")]
        public RollbackConfigV1Beta1 RollbackTo { get; set; }

        /// <summary>
        ///     The annotations to be updated to a deployment
        /// </summary>
        [YamlMember(Alias = "updatedAnnotations")]
        [JsonProperty("updatedAnnotations", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> UpdatedAnnotations { get; set; } = new Dictionary<string, string>();
    }
}
