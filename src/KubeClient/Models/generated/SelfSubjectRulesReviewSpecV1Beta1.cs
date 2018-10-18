using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class SelfSubjectRulesReviewSpecV1Beta1
    {
        /// <summary>
        ///     Namespace to evaluate rules for. Required.
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }
    }
}
