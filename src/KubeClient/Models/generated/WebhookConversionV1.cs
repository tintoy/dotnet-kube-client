using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     WebhookConversion describes how to call a conversion webhook
    /// </summary>
    public partial class WebhookConversionV1
    {
        /// <summary>
        ///     clientConfig is the instructions for how to call the webhook if strategy is `Webhook`.
        /// </summary>
        [YamlMember(Alias = "clientConfig")]
        [JsonProperty("clientConfig", NullValueHandling = NullValueHandling.Ignore)]
        public WebhookClientConfigV1 ClientConfig { get; set; }

        /// <summary>
        ///     conversionReviewVersions is an ordered list of preferred `ConversionReview` versions the Webhook expects. The API server will use the first version in the list which it supports. If none of the versions specified in this list are supported by API server, conversion will fail for the custom resource. If a persisted Webhook configuration specifies allowed versions and does not include any versions known to the API Server, calls to the webhook will fail.
        /// </summary>
        [YamlMember(Alias = "conversionReviewVersions")]
        [JsonProperty("conversionReviewVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ConversionReviewVersions { get; } = new List<string>();
    }
}
