using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeObject("SelfSubjectRulesReviewSpec", "v1")]
    public partial class SelfSubjectRulesReviewSpecV1
    {
        /// <summary>
        ///     Namespace to evaluate rules for. Required.
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}
