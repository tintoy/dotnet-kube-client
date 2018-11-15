using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Webhook describes an admission webhook and the resources and operations it applies to.
    /// </summary>
    public partial class WebhookV1Beta1
    {
        /// <summary>
        ///     The name of the admission webhook. Name should be fully qualified, e.g., imagepolicy.kubernetes.io, where "imagepolicy" is the name of the webhook, and kubernetes.io is the name of the organization. Required.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     ClientConfig defines how to communicate with the hook. Required
        /// </summary>
        [YamlMember(Alias = "clientConfig")]
        [JsonProperty("clientConfig", NullValueHandling = NullValueHandling.Include)]
        public WebhookClientConfigV1Beta1 ClientConfig { get; set; }

        /// <summary>
        ///     NamespaceSelector decides whether to run the webhook on an object based on whether the namespace for that object matches the selector. If the object itself is a namespace, the matching is performed on object.metadata.labels. If the object is another cluster scoped resource, it never skips the webhook.
        ///     
        ///     For example, to run the webhook on any objects whose namespace is not associated with "runlevel" of "0" or "1";  you will set the selector as follows: "namespaceSelector": {
        ///       "matchExpressions": [
        ///         {
        ///           "key": "runlevel",
        ///           "operator": "NotIn",
        ///           "values": [
        ///             "0",
        ///             "1"
        ///           ]
        ///         }
        ///       ]
        ///     }
        ///     
        ///     If instead you want to only run the webhook on any objects whose namespace is associated with the "environment" of "prod" or "staging"; you will set the selector as follows: "namespaceSelector": {
        ///       "matchExpressions": [
        ///         {
        ///           "key": "environment",
        ///           "operator": "In",
        ///           "values": [
        ///             "prod",
        ///             "staging"
        ///           ]
        ///         }
        ///       ]
        ///     }
        ///     
        ///     See https://kubernetes.io/docs/concepts/overview/working-with-objects/labels/ for more examples of label selectors.
        ///     
        ///     Default to the empty LabelSelector, which matches everything.
        /// </summary>
        [YamlMember(Alias = "namespaceSelector")]
        [JsonProperty("namespaceSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 NamespaceSelector { get; set; }

        /// <summary>
        ///     Rules describes what operations on what resources/subresources the webhook cares about. The webhook cares about an operation if it matches _any_ Rule. However, in order to prevent ValidatingAdmissionWebhooks and MutatingAdmissionWebhooks from putting the cluster in a state which cannot be recovered from without completely disabling the plugin, ValidatingAdmissionWebhooks and MutatingAdmissionWebhooks are never called on admission requests for ValidatingWebhookConfiguration and MutatingWebhookConfiguration objects.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<RuleWithOperationsV1Beta1> Rules { get; } = new List<RuleWithOperationsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Rules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRules() => Rules.Count > 0;

        /// <summary>
        ///     FailurePolicy defines how unrecognized errors from the admission endpoint are handled - allowed values are Ignore or Fail. Defaults to Ignore.
        /// </summary>
        [YamlMember(Alias = "failurePolicy")]
        [JsonProperty("failurePolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string FailurePolicy { get; set; }
    }
}
