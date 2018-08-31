using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DeploymentList is a list of Deployments.
    /// </summary>
    [KubeListItem("Deployment", "apps/v1beta1")]
    [KubeObject("DeploymentList", "apps/v1beta1")]
    public partial class DeploymentListV1Beta1 : Models.DeploymentListV1Beta1, ITracked
    {
        /// <summary>
        ///     Items is the list of Deployments.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.DeploymentV1Beta1> Items { get; } = new List<Models.DeploymentV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
