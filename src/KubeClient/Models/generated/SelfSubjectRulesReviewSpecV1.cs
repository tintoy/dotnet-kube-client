using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SelfSubjectRulesReviewSpec defines the specification for SelfSubjectRulesReview.
    /// </summary>
    public partial class SelfSubjectRulesReviewSpecV1
    {
        /// <summary>
        ///     Namespace to evaluate rules for. Required.
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }
    }
}
