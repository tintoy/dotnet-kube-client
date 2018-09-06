using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A scope selector represents the AND of the selectors represented by the scoped-resource selector requirements.
    /// </summary>
    public partial class ScopeSelectorV1
    {
        /// <summary>
        ///     A list of scope selector requirements by scope of the resources.
        /// </summary>
        [YamlMember(Alias = "matchExpressions")]
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public List<ScopedResourceSelectorRequirementV1> MatchExpressions { get; set; } = new List<ScopedResourceSelectorRequirementV1>();
    }
}
