using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Deployment enables declarative updates for Pods and ReplicaSets.
    /// </summary>
    [KubeObject("Deployment", "apps/v1beta1")]
    public partial class DeploymentV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the Deployment.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public virtual DeploymentSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the Deployment.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public virtual DeploymentStatusV1Beta1 Status { get; set; }
    }
}
