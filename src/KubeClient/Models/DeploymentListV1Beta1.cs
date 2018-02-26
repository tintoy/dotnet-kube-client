using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentList is a list of Deployments.
    /// </summary>
    [KubeObject("DeploymentList", "apps/v1beta1")]
    public class DeploymentListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is the list of Deployments.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<DeploymentV1Beta1> Items { get; set; } = new List<DeploymentV1Beta1>();
    }
}
