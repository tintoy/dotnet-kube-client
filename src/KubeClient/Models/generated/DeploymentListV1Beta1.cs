using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentList is a list of Deployments.
    /// </summary>
    [KubeListItem("Deployment", "apps/v1beta1")]
    [KubeObject("DeploymentList", "apps/v1beta1")]
    public partial class DeploymentListV1Beta1 : KubeResourceListV1<DeploymentV1Beta1>
    {
        /// <summary>
        ///     Items is the list of Deployments.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<DeploymentV1Beta1> Items { get; } = new List<DeploymentV1Beta1>();
    }
}
