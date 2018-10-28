using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MutatingWebhookConfiguration describes the configuration of and admission webhook that accept or reject and may change the object.
    /// </summary>
    [KubeObject("MutatingWebhookConfiguration", "admissionregistration.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations")]
    [KubeApi(KubeAction.Create, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations")]
    [KubeApi(KubeAction.Get, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.Delete, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.Update, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/admissionregistration.k8s.io/v1beta1/watch/mutatingwebhookconfigurations")]
    [KubeApi(KubeAction.DeleteCollection, "apis/admissionregistration.k8s.io/v1beta1/mutatingwebhookconfigurations")]
    [KubeApi(KubeAction.Watch, "apis/admissionregistration.k8s.io/v1beta1/watch/mutatingwebhookconfigurations/{name}")]
    public partial class MutatingWebhookConfigurationV1Beta1 : KubeResourceV1
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
