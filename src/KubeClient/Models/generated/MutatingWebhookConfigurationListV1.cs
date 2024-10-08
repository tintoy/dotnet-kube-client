using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MutatingWebhookConfigurationList is a list of MutatingWebhookConfiguration.
    /// </summary>
    [KubeListItem("MutatingWebhookConfiguration", "admissionregistration.k8s.io/v1")]
    [KubeObject("MutatingWebhookConfigurationList", "admissionregistration.k8s.io/v1")]
    public partial class MutatingWebhookConfigurationListV1 : KubeResourceListV1<MutatingWebhookConfigurationV1>
    {
        /// <summary>
        ///     List of MutatingWebhookConfiguration.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<MutatingWebhookConfigurationV1> Items { get; } = new List<MutatingWebhookConfigurationV1>();
    }
}
