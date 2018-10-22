using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingWebhookConfigurationList is a list of ValidatingWebhookConfiguration.
    /// </summary>
    [KubeListItem("ValidatingWebhookConfiguration", "admissionregistration.k8s.io/v1beta1")]
    [KubeObject("ValidatingWebhookConfigurationList", "admissionregistration.k8s.io/v1beta1")]
    public partial class ValidatingWebhookConfigurationListV1Beta1 : KubeResourceListV1<ValidatingWebhookConfigurationV1Beta1>
    {
        /// <summary>
        ///     List of ValidatingWebhookConfiguration.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ValidatingWebhookConfigurationV1Beta1> Items { get; } = new List<ValidatingWebhookConfigurationV1Beta1>();
    }
}
