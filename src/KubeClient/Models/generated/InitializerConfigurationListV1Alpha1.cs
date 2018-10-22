using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     InitializerConfigurationList is a list of InitializerConfiguration.
    /// </summary>
    [KubeListItem("InitializerConfiguration", "admissionregistration.k8s.io/v1alpha1")]
    [KubeObject("InitializerConfigurationList", "admissionregistration.k8s.io/v1alpha1")]
    public partial class InitializerConfigurationListV1Alpha1 : KubeResourceListV1<InitializerConfigurationV1Alpha1>
    {
        /// <summary>
        ///     List of InitializerConfiguration.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<InitializerConfigurationV1Alpha1> Items { get; } = new List<InitializerConfigurationV1Alpha1>();
    }
}
