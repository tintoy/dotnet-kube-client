using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeObject("SelfSubjectRulesReviewSpec", "v1beta1")]
    public partial class SelfSubjectRulesReviewSpecV1Beta1
    {
        /// <summary>
        ///     Namespace to evaluate rules for. Required.
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}
