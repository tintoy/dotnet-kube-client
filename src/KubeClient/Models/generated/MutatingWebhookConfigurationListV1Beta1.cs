using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MutatingWebhookConfigurationList is a list of MutatingWebhookConfiguration.
    /// </summary>
    [KubeListItem("MutatingWebhookConfiguration", "v1beta1")]
    [KubeObject("MutatingWebhookConfigurationList", "v1beta1")]
    public partial class MutatingWebhookConfigurationListV1Beta1 : KubeResourceListV1<MutatingWebhookConfigurationV1Beta1>
    {
        /// <summary>
        ///     List of MutatingWebhookConfiguration.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<MutatingWebhookConfigurationV1Beta1> Items { get; } = new List<MutatingWebhookConfigurationV1Beta1>();
    }
}
