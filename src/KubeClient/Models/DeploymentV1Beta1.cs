using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Deployment enables declarative updates for Pods and ReplicaSets.
    /// </summary>
    [KubeObject("Deployment", "v1beta1")]
    public class DeploymentV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the Deployment.
        /// </summary>
        [JsonProperty("spec")]
        public DeploymentSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the Deployment.
        /// </summary>
        [JsonProperty("status")]
        public DeploymentStatusV1Beta1 Status { get; set; }
    }
}
