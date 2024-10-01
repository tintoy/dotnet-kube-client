using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceConversion describes how to convert different versions of a CR.
    /// </summary>
    public partial class CustomResourceConversionV1
    {
        /// <summary>
        ///     webhook describes how to call the conversion webhook. Required when `strategy` is set to `"Webhook"`.
        /// </summary>
        [YamlMember(Alias = "webhook")]
        [JsonProperty("webhook", NullValueHandling = NullValueHandling.Ignore)]
        public WebhookConversionV1 Webhook { get; set; }

        /// <summary>
        ///     strategy specifies how custom resources are converted between versions. Allowed values are: - `"None"`: The converter only change the apiVersion and would not touch any other field in the custom resource. - `"Webhook"`: API Server will call to an external webhook to do the conversion. Additional information
        ///       is needed for this option. This requires spec.preserveUnknownFields to be false, and spec.conversion.webhook to be set.
        /// </summary>
        [YamlMember(Alias = "strategy")]
        [JsonProperty("strategy", NullValueHandling = NullValueHandling.Include)]
        public string Strategy { get; set; }
    }
}
