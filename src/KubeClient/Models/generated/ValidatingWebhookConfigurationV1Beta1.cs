using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingWebhookConfiguration describes the configuration of and admission webhook that accept or reject and object without changing it.
    /// </summary>
    [KubeObject("ValidatingWebhookConfiguration", "admissionregistration.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations")]
    [KubeApi(KubeAction.Create, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations")]
    [KubeApi(KubeAction.Get, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.Delete, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.Update, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/admissionregistration.k8s.io/v1beta1/watch/validatingwebhookconfigurations")]
    [KubeApi(KubeAction.DeleteCollection, "apis/admissionregistration.k8s.io/v1beta1/validatingwebhookconfigurations")]
    [KubeApi(KubeAction.Watch, "apis/admissionregistration.k8s.io/v1beta1/watch/validatingwebhookconfigurations/{name}")]
    public partial class ValidatingWebhookConfigurationV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Webhooks is a list of webhooks and the affected resources and operations.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "webhooks")]
        [JsonProperty("webhooks", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<WebhookV1Beta1> Webhooks { get; } = new List<WebhookV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Webhooks"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeWebhooks() => Webhooks.Count > 0;
    }
}
